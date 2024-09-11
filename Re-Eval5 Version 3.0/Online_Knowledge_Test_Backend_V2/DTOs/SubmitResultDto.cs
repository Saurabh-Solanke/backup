namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class SubmitResultDto
    {
        public int ExamId { get; set; }  // Foreign Key to Exam
        public string UserId { get; set; }  // Foreign Key to ApplicationUser
        public int Duration { get; set; }  // Duration in minutes
        public int markforreview { get; set; }
        public List<UserAnswerDto> UserAnswers { get; set; }
    }
}
