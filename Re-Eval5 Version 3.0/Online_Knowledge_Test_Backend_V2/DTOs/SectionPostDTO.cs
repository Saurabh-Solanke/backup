namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class SectionPostDTO
    {
        public int ExamId { get; set; }              // Foreign key to Exam
        public string Title { get; set; }            // Section title (e.g., Math, Science)
        public int NumberOfQuestions { get; set; }   // Number of questions in the section
        public int TotalMarks { get; set; }          // Total marks for the section
        public int PassingMarks { get; set; }        // Minimum marks required to pass
        public decimal Weightage { get; set; }
        public List<QuestionPostDTO> Questions { get; set; }   // List of questions in the section
    }
}
