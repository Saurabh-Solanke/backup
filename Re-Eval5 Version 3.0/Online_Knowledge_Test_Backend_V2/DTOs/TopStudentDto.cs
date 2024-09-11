namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class TopStudentDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int TotalScore { get; set; }

        public int AttemptNumber { get; set; }
        public double Percentile { get; set; }
    }
}
