using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge__Test_Backend.Models;
using Online_Knowledge__Test_Backend.RepositoryLayer;

namespace Online_Knowledge__Test_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExamController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllSubjects")]
        public async Task<ActionResult<IEnumerable<GetSubjectDto>>> GetAllSubject()
        {
            try
            {
                IEnumerable<Subject> subjectList = await _unitOfWork.SubjectRepository.GetAllSubjects();
                IEnumerable<GetSubjectDto> getSubjectList = _mapper.Map<IEnumerable<GetSubjectDto>>(subjectList);
                return Ok(getSubjectList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("GetExamQuestionAndAnswer")]
        public async Task<ActionResult<IEnumerable<QuestionAndAnswerDTO>>> GetExamQuestionAndAnswer()
        {
            try
            {
                return Ok(await _unitOfWork.TestRepository.GetAllQuestionAnswer());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetQuestionsBySubject")]
        public async Task<ActionResult<SubjectDTO>> GetQuestionsBySubject(int subjectId)
        {
            try
            {
                var data = await _unitOfWork.TestRepository.GetQuestionsBySubjectIdAsync(subjectId);
                return Ok(data);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("submitExam")]
        public async Task<IActionResult> SubmitExam([FromBody] ExamSubmissionDto submission)
        {
            if (submission == null || submission.Responses == null || !submission.Responses.Any())
            {
                return BadRequest("Invalid submission data.");
            }

            var result = await CalculateResult(submission.Responses);

            return Ok(result);
        }

        private async Task<ExamResultDto> CalculateResult(List<QuestionResponseDto> responses)
        {
            int correct = 0;
            int incorrect = 0;
            int attempted = 0;

            // Retrieve all questions with their options
            var questions = await _unitOfWork.TestRepository.GetAllQuestionAnswer();

            foreach (var response in responses)
            {
                // Find the corresponding question by QuestionId
                var question = questions.FirstOrDefault(q => q.Id == response.QuestionId);
                if (question != null)
                {
                    attempted++;

                    // Get the correct options for the current question
                    var correctOptions = question.Answer.ToList();

                    // Ensure both correctOptions and response.SelectedOptions are not null
                    if (correctOptions != null && response.SelectedOptions != null)
                    {
                        // Check if the selected options match the correct options
                        bool isCorrect;

                        if (question.IsMultiple)
                        {
                            // For multiple choice questions, check if all selected options are correct and vice versa
                            isCorrect = correctOptions.Count() == response.SelectedOptions.Count &&
                                        !correctOptions.Except(response.SelectedOptions).Any() &&
                                        !response.SelectedOptions.Except(correctOptions).Any();
                        }
                        else
                        {
                            // For single choice questions, just check if the single selected option matches the correct option
                            isCorrect = correctOptions.Count == 1 &&
                                        correctOptions.First() == response.SelectedOptions.FirstOrDefault();
                        }

                        if (isCorrect)
                        {
                            correct++;
                        }
                        else
                        {
                            incorrect++;
                        }
                    }
                }
            }

            var totalQuestions = questions.Count();
            var unattempted = totalQuestions - attempted;
            var percentage = (double)correct / totalQuestions * 100;

            return new ExamResultDto
            {
                TotalQuestions = totalQuestions,
                Attempted = attempted,
                Unattempted = unattempted,
                Correct = correct,
                Incorrect = incorrect,
                Percentage = percentage
            };
        }

        // method for saving the result in database
        [HttpPost("SaveExamDataInDB")]
        public async Task<IActionResult> SaveExamDataInDB([FromBody] ResultDataDto resultDataDto)
        {
            var data = new Test
            {
                SubjectName = resultDataDto.SubjectName,
                SubjectId = resultDataDto.SubjectId,
                UserId = resultDataDto.UserId,
                ConductedOn = DateTime.Now
            };

            Test task = await _unitOfWork.TestRepository.SaveTaskData(data);
            int testID = task.TestId;

            var result = new Result
            {
                UnAttemptedQuestions = resultDataDto.UnAttemptedQuestions,
                TotalQuestions = resultDataDto.TotalQuestions,
                TestId = testID,
                UserId = resultDataDto.UserId,
                AttemptedQuestions = resultDataDto.AttemptedQuestions,
                CorrectQuestions = resultDataDto.CorrectQuestions,
                InCorrectQuestions = resultDataDto.InCorrectQuestions,
                PercentageObtained = Convert.ToDecimal(resultDataDto.PercentageObtained),
                CreatedOn = DateTime.Now,

            };
            await _unitOfWork.ResultRepository.AddAsync(result);
            return Ok();
        }



        [HttpPost("SaveUserTestAnswers")]
        public async Task<IActionResult> SaveUserTestAnswers([FromBody] List<UserTestAnswerDto> userTestAnswers)
        {
            if (userTestAnswers == null || !userTestAnswers.Any())
            {
                return BadRequest("No answers provided.");
            }

            foreach (var answerDto in userTestAnswers)
            {
                var userTestAnswer = new UserTestAnswer
                {
                    TestId = answerDto.TestId,
                    QuestionId = answerDto.QuestionId,
                    OptionId = answerDto.OptionId,
                    IsSelected = answerDto.IsSelected
                };

                await _unitOfWork.UserTestAnswerRepository.AddAsync(userTestAnswer);
            }
            return Ok("Answers saved successfully.");
        }


        //[HttpPost("StartExam")]
        //public async Task<IActionResult> StartExam([FromBody] CreateTestDto createTestDto)
        //{
        //    if (createTestDto == null)
        //    {
        //        return BadRequest("Invalid test data.");
        //    }

        //    // Create the new test entity
        //    var test = new Test
        //    {
        //        SubjectName = createTestDto.SubjectName,
        //        ConductedOn = DateTime.Now, // Set the current date and time
        //        UserId = createTestDto.UserId,
        //        SubjectId = createTestDto.SubjectId
        //    };

        //    // Save the test to the database
        //    var createdTest = await _unitOfWork.TestRepository.SaveTaskData(test);

        //    return Ok(createdTest);
        //}


        [HttpGet("GetUserTestAnswers")]
        public async Task<IActionResult> GetUserTestAnswers(int testId)
        {
            var answers = await _unitOfWork.UserTestAnswerRepository.GetAnswersByTestIdAsync(testId);
            return Ok(answers);
        }
    }
}
