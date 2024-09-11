using AutoMapper;
using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Services.Implementation
{
    public class ExamResultService : IExamResultService
    {
        private readonly IExamResultRepository _examResultRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public ExamResultService(
            IExamResultRepository examResultRepository,
            IQuestionRepository questionRepository,
            IMapper mapper)
        {
            _examResultRepository = examResultRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<SubmitExamResultDto> SubmitResultAsync(SubmitResultDto submitResultDto)
        {
            // Business logic for submitting the exam result, previously in the controller
            var sections = await _questionRepository.GetSectionsWithQuestionsAsync(submitResultDto.ExamId);

            decimal totalObtainedMarks = 0;
            var userAnswers = new List<UserAnswer>();
            var sectionResults = new List<SectionResult>();

            foreach (var section in sections)
            {
                decimal sectionObtainedMarks = 0;
                int attemptedQuestions = 0;
                int correctAnswers = 0;

                foreach (var question in section.Questions)
                {
                    var userAnswerDto = submitResultDto.UserAnswers.FirstOrDefault(ua => ua.QuestionId == question.QuestionId);
                    if (userAnswerDto == null) continue;

                    attemptedQuestions++;

                    var options = await _questionRepository.GetOptionsByQuestionIdAsync(question.QuestionId);
                    decimal positiveMarks = 0;
                    decimal negativeMarks = 0;

                    foreach (var option in options)
                    {
                        if (userAnswerDto.SelectedOptionIds.Contains(option.OptionId))
                        {
                            if (option.IsCorrect)
                            {
                                positiveMarks += option.Marks ?? 0;
                                correctAnswers++;
                            }
                            else
                            {
                                negativeMarks += (option.Marks ?? 0) * (section.Weightage ?? 1.0M);
                            }
                        }
                    }

                    sectionObtainedMarks += positiveMarks + negativeMarks;
                }

                var sectionScore = sectionObtainedMarks;
                var isPassed = sectionScore >= section.passingMarks;

                sectionResults.Add(new SectionResult
                {
                    SectionId = section.SectionId,
                    AttemptedQuestions = attemptedQuestions,
                    CorrectAnswers = correctAnswers,
                    SectionScore = (int)Math.Round(sectionScore),
                    IsPassed = isPassed
                });

                totalObtainedMarks += sectionObtainedMarks;
            }

            var percentage = totalObtainedMarks / sections.Sum(s => s.TotalMarks) * 100;
            var passed = percentage >= 50;

            var latestAttemptNumber = await _examResultRepository.GetLatestAttemptNumberAsync(submitResultDto.UserId, submitResultDto.ExamId);

            var examResult = new ExamResult
            {
                UserId = submitResultDto.UserId,
                ExamId = submitResultDto.ExamId,
                AttemptNumber = latestAttemptNumber + 1,
                TotalScore = (int)Math.Round(totalObtainedMarks),
                Percentage = (double)percentage,
                Passed = passed,
                CompletedDate = DateTime.UtcNow,
                Duration = submitResultDto.Duration,
                markforreview = submitResultDto.markforreview
            };

            var savedExamResult = await _examResultRepository.SubmitExamResultAsync(examResult);
            sectionResults.ForEach(sr => sr.ExamResultId = savedExamResult.ExamResultId);
            await _examResultRepository.AddSectionResultsAsync(sectionResults);

            userAnswers.ForEach(ua => ua.ResultId = savedExamResult.ExamResultId);
            await _examResultRepository.AddUserAnswersAsync(userAnswers);

            return _mapper.Map<SubmitExamResultDto>(savedExamResult);
        }

        public async Task<IEnumerable<SubmitExamResultDto>> GetAllResultsAsync()
        {
            var examResults = await _examResultRepository.GetAllResultsAsync();
            return _mapper.Map<IEnumerable<SubmitExamResultDto>>(examResults);
        }

        public async Task<IEnumerable<SubmitExamResultDto>> GetResultsByUserAsync(string userId)
        {
            var examResults = await _examResultRepository.GetResultsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<SubmitExamResultDto>>(examResults);
        }
    }    

}
