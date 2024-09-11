namespace PassportApi.Dtos.Auth
{
    public class LoggedInUser
    {
        public int? UserId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string Email { get; set; }
        public string? MobileNo { get; set; }

        public string Role { get; set; }
        public string Token {  get; set; }
        public string PassportUserId { get; set; }

        public DateTime Expiration {  get; set; }
    }
}
