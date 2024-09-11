namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class SectionGetDTO
    {
        public int SectionId { get; set; }           // Section identifier
        public int ExamId { get; set; }              // Foreign key to Exam
        public string Title { get; set; }            // Section title
        public int NumberOfQuestions { get; set; }   // Number of questions in the section
        public int TotalMarks { get; set; }          // Total marks for the section
        public int PassingMarks { get; set; }        // Minimum marks required to pass
        public decimal Weightage { get; set; }
        public List<QuestionGetDTO> Questions { get; set; }   // List of questions in the section
    }
}
