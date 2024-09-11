namespace MCQExamApi.Dtos.Auth
{
    public class LoggedInUser
    {
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string Email { get; set; }
        public string? MobileNo { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }
        public string ExamUserId { get; set; }

        public DateTime Expiration { get; set; }
    }
}
