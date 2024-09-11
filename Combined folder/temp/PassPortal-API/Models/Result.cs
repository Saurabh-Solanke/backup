
namespace PassPortal_API.Models
{

    public class Result
    {
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}