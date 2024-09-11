namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class CreateSectionDto
    {
        public int ExamId { get; set; }  // Foreign key to Exam
        public string Title { get; set; }
        public int TotalMarks { get; set; }
    }
}
