namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class ExamResultDto
    {
        public int ExamResultId { get; set; }
        public int markforreview { get; set; }
        public int TotalQuestions { get; set; }
        public string ExamTitle { get; set; }

        // New fields for user information
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        // Additional exam result fields
        public DateTime CompletedDate { get; set; }
        public int TotalScore { get; set; }
        public bool Passed { get; set; }
    }
}
