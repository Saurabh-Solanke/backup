namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class ReportTwoDto
    {
        public int ExamResultId { get; set; }
        public string UserName { get; set; }  // Replace UserId with UserName
        public string UserEmail { get; set; } // Add UserEmail
        public string ExamTitle { get; set; }
        public int TotalScore { get; set; }
        public double Percentage { get; set; }
        public bool Passed { get; set; }
        public DateTime CompletedDate { get; set; }
        public int Duration { get; set; }
    }
}
