using MCQExamApi.Dtos.Question;

namespace MCQExamApi.Dtos.StudentExam
{
    public class StudentExamResultDTO
    {
        public int StudentExamId { get; set; }
        public int TotalQuestions { get; set; }
        public int QuestionsAnswered { get; set; }
        public int CorrectAnswers { get; set; }
        public int IncorrectAnswers { get; set; }
        public int SkippedQuestions { get; set; }
        public decimal TotalScore { get; set; }
        public decimal ObtainedScore { get; set; }
        public string ExamStatus { get; set; } // Passed/Failed
        public string TimeTaken { get; set; } // Time in format "45 mins"
        public List<QuestionResultDTO> QuestionResults { get; set; } = new List<QuestionResultDTO>();
    }
}
