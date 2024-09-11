using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MCQExamApi.Data;
using MCQExamApi.Models;
using AutoMapper;
using MCQExamApi.Dtos.StudentExam;
using MCQExamApi.interfaces;
using MCQExamApi.Dtos.Option;
using MCQExamApi.Dtos.Question;

namespace MCQExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamsController : ControllerBase
    {
        private readonly IRootRepository<StudentExam> _studentExamRepo;
        private readonly IRootRepository<StudentAnswer> _studentAnswerRepo;
        private readonly IMapper _mapper;
        private readonly IRootRepository<Question> _questionRepo;
        private readonly IRootRepository<Result> _resultRepo;
        private readonly IRootRepository<Option> _optionRepo;

        public StudentExamsController(IRootRepository<StudentExam> studentExamRepo, IRootRepository<StudentAnswer> studentAnswerRepo,
            IMapper mapper, IRootRepository<Question> questionRepo, IRootRepository<Result> resultRepo, IRootRepository<Option> optionRepo)
        {
            _studentExamRepo = studentExamRepo;
            _studentAnswerRepo = studentAnswerRepo;
            _mapper = mapper;
            _questionRepo = questionRepo;
            _resultRepo = resultRepo;
            _optionRepo = optionRepo;
        }

        // GET: api/StudentExams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentExamDTO>>> GetStudentExams()
        {
            try
            {
                var studentExams = await _studentExamRepo.GetAllAsync();
                var studentExamDtos = _mapper.Map<ICollection<StudentExamDTO>>(studentExams);
                return Ok(studentExamDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while fetching student exams.");
            }
        }
        [HttpGet("id")]
        public async Task<ActionResult<StudentExamDTO>> GetStudentExamById(int studentExamId)
        {
            try
            {
                var studentExam = await _studentExamRepo.GetByIdAsync(studentExamId);
                if (studentExam == null)
                {
                    return NotFound($"StudentExam with ID {studentExamId} not found.");
                }
                var createdStudentExamDto = _mapper.Map<StudentExamDTO>(studentExam);
                return Ok(createdStudentExamDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the exam results.");
            }

        }
        // GET: api/StudentExams/5
        [HttpGet("result/{studentExamId}")]
        public async Task<ActionResult<StudentExamDTO>> GetStudentExam(int studentExamId)
        {
            try
            {
                // Fetch StudentExam, StudentAnswers, and related entities
                var studentExam = await _studentExamRepo.GetByIdAsync(studentExamId);
                if (studentExam == null)
                {
                    return NotFound($"StudentExam with ID {studentExamId} not found.");
                }

                var result = await _resultRepo.GetResultByStudentExamIdAsync(studentExamId);
                var studentAnswers = await _studentAnswerRepo.GetStudentAnswerByStudentExamIdAsync(studentExamId);
                var questions = await _questionRepo.GetQuestionsByExamIdAsync(studentExam.ExamId);

                decimal obtainedScore = 0;
                int correctAnswers = 0;
                int incorrectAnswers = 0;

                // Initialize DTO before usage
                var studentExamResultDTO = new StudentExamResultDTO
                {
                    StudentExamId = studentExam.StudentExamId,
                    TotalQuestions = questions.Count(),
                    QuestionsAnswered = studentAnswers.Count(),
                    CorrectAnswers = 0,
                    IncorrectAnswers = 0,
                    SkippedQuestions = questions.Count() - studentAnswers.Count(),
                    TotalScore = questions.Count() * 2, // Assuming 2 points per question
                    ObtainedScore = 0,
                    ExamStatus = "",
                    TimeTaken = $"{Math.Floor((studentExam.EndTime - studentExam.StartTime)?.TotalMinutes ?? 0)} mins",
                    QuestionResults = new List<QuestionResultDTO>()
                };

                // Calculate total and obtained scores
                foreach (var question in questions)
                {
                    var questionResult = new QuestionResultDTO
                    {
                        QuestionId = question.QuestionId,
                        QuestionText = question.QuestionText
                    };

                    // Retrieve the correct options for this question
                    var correctOptions = await _optionRepo.GetCorrectOptionsByQuestionIdAsync(question.QuestionId);
                    var studentAnswer = studentAnswers.FirstOrDefault(sa => sa.QuestionId == question.QuestionId);

                    if (question.QuestionType == "SingleAnswer")
                    {
                        if (studentAnswer?.SelectedOptionId.HasValue == true &&
                            correctOptions.Any(co => co.OptionId == studentAnswer.SelectedOptionId.Value))
                        {
                            obtainedScore += 2; // Full points for correct single-answer question
                            correctAnswers++;
                            questionResult.PointsScored = 2;
                        }
                        else if (studentAnswer?.SelectedOptionId.HasValue == true)
                        {
                            incorrectAnswers++;
                            questionResult.PointsScored = 0;
                        }
                        else
                        {
                            questionResult.PointsScored = 0; // Skipped or incorrect
                        }
                    }
                    else if (question.QuestionType == "MultipleAnswers")
                    {
                        // Split selected option IDs and convert them to integers
                        var selectedOptionIds = studentAnswer?.SelectedOptionIds?.Split(',').Select(int.Parse).ToList() ?? new List<int>();

                        int correctSelections = selectedOptionIds.Intersect(correctOptions.Select(co => co.OptionId)).Count();
                        int incorrectSelections = selectedOptionIds.Except(correctOptions.Select(co => co.OptionId)).Count();

                        // Each correct selection earns 0.5 points
                        decimal pointsForCorrect = correctSelections * 0.5m;
                        obtainedScore += pointsForCorrect;

                        questionResult.PointsScored = pointsForCorrect;

                        if (correctSelections == correctOptions.Count() && incorrectSelections == 0)
                        {
                            correctAnswers++;
                        }
                        else if (selectedOptionIds.Any())
                        {
                            incorrectAnswers++;
                        }
                    }

                    // Populate options for each question
                    foreach (var option in question.Options)
                    {
                        questionResult.Options.Add(new OptionResultDTO
                        {
                            OptionId = option.OptionId,
                            OptionText = option.OptionText,
                            IsCorrect = option.IsCorrect,
                            IsSelected = studentAnswer != null &&
                                         (studentAnswer.SelectedOptionId == option.OptionId ||
                                          (studentAnswer.SelectedOptionIds != null &&
                                           studentAnswer.SelectedOptionIds.Split(',').Select(int.Parse).Contains(option.OptionId)))
                        });
                    }

                    studentExamResultDTO.QuestionResults.Add(questionResult);
                }

                // Finalize DTO with scores and status
                studentExamResultDTO.ObtainedScore = obtainedScore;
                studentExamResultDTO.CorrectAnswers = correctAnswers;
                studentExamResultDTO.IncorrectAnswers = incorrectAnswers;
                studentExamResultDTO.ExamStatus = obtainedScore >= studentExamResultDTO.TotalScore * 0.5m ? "Passed" : "Failed";

                return Ok(studentExamResultDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the exam results.");
            }
        }

        // PUT: api/StudentExams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentExam(int id, StudentExamDTO studentExamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentExamDto.StudentExamId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var existingStudentExam = await _studentExamRepo.GetByIdAsync(id);
                if (existingStudentExam == null)
                {
                    return NotFound($"Student exam with ID {id} not found.");
                }

                var studentExam = _mapper.Map(studentExamDto, existingStudentExam); // Update the existing entity with the DTO data
                await _studentExamRepo.UpdateAsync(id, studentExam);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the student exam.");
            }
        }

        // POST: api/StudentExams
        [HttpPost]
        public async Task<ActionResult<StudentExamDTO>> PostStudentExam([FromBody] StudentExamDTO studentExamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var studentExam = _mapper.Map<StudentExam>(studentExamDto);
                var createdStudentExam = await _studentExamRepo.AddAsync(studentExam);

                if (createdStudentExam.StudentExamId <= 0)
                {
                    return BadRequest("Invalid StudentExamId.");
                }

                decimal obtainedScore = 0;
                int totalQuestions = studentExamDto.Answers.Count;
                decimal totalScore = totalQuestions * 2; // Each question is worth 2 points

                // Add StudentAnswer records and calculate scores
                foreach (var answerDto in studentExamDto.Answers)
                {
                    var studentAnswer = new StudentAnswer
                    {
                        StudentExamId = createdStudentExam.StudentExamId,
                        QuestionId = answerDto.QuestionId,
                        SelectedOptionId = answerDto.SelectedOptionId,
                        SelectedOptionIds = answerDto.SelectedOptionIds
                    };

                    await _studentAnswerRepo.AddAsync(studentAnswer);

                    // Calculate obtained score based on the answer type
                    var question = await _questionRepo.GetByIdAsync(answerDto.QuestionId); // Assuming there's a repository for Questions
                    if (question.QuestionType == "SingleAnswer" && answerDto.SelectedOptionId.HasValue)
                    {
                        var correctOption = await _optionRepo.GetByIdAsync(answerDto.SelectedOptionId.Value); // Assuming OptionRepo exists
                        if (correctOption != null && correctOption.IsCorrect)
                        {
                            obtainedScore += 2; // Full points for correct single-answer question
                        }
                    }
                    else if (question.QuestionType == "MultipleAnswers" && !string.IsNullOrEmpty(answerDto.SelectedOptionIds))
                    {
                        var selectedOptionIds = answerDto.SelectedOptionIds.Split(',').Select(int.Parse);
                        var correctOptions = await _optionRepo.GetCorrectOptionsByQuestionIdAsync(answerDto.QuestionId);

                        foreach (var selectedOptionId in selectedOptionIds)
                        {
                            if (correctOptions.Any(o => o.OptionId == selectedOptionId))
                            {
                                obtainedScore += 0.5M; // Partial points for each correct selection
                            }
                        }
                    }
                }

                // Save the result
                var result = new Result
                {
                    StudentExamId = createdStudentExam.StudentExamId,
                    TotalScore = totalScore,
                    ObtainedScore = obtainedScore
                };
                await _resultRepo.AddAsync(result); // Assuming a repository for Results

                var createdStudentExamDto = _mapper.Map<StudentExamDTO>(createdStudentExam);

                return CreatedAtAction(nameof(GetStudentExamById), new { id = createdStudentExamDto.StudentExamId }, createdStudentExamDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the student exam.");
            }
        }


        // DELETE: api/StudentExams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentExam(int id)
        {
            try
            {
                var studentExam = await _studentExamRepo.GetByIdAsync(id);
                if (studentExam == null)
                {
                    return NotFound($"Student exam with ID {id} not found.");
                }

                await _studentExamRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the student exam.");
            }
        }
    }
}
