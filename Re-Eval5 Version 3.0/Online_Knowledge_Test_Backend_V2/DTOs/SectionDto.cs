namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class SectionDto
    {
        public int SectionId { get; set; }
        public int ExamId { get; set; }
        public string Title { get; set; }
        public int TotalMarks { get; set; }
        public int NumberOfQuestions { get; set; }
        public int passingMarks { get; set; }
        public decimal Weightage { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }

    }
}
