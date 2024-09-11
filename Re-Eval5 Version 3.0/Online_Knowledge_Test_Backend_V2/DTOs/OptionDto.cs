namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class OptionDto
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public int Marks { get; set; }
    }
}
