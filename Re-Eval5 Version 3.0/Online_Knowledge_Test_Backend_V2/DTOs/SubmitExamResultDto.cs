namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class SubmitExamResultDto
    {
        public int ExamResultId { get; set; }
        public string UserId { get; set; }
        public int ExamId { get; set; }
        public int AttemptNumber { get; set; }
        public int TotalScore { get; set; }
        public double Percentage { get; set; }
        public bool Passed { get; set; }
        public int Duration { get; set; }  // Duration in minutes
        public int markforreview { get; set; }  // Added field
        public DateTime CompletedDate { get; set; }
        public List<SectionResultDto> SectionResults { get; set; }
    }
}
