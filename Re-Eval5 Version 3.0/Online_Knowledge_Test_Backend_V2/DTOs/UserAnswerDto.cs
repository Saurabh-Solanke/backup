namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class UserAnswerDto
    {
        public int QuestionId { get; set; }
        public List<int> SelectedOptionIds { get; set; }
    }
}
