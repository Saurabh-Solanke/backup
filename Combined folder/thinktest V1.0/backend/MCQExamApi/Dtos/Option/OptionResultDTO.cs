namespace MCQExamApi.Dtos.Option
{
    public class OptionResultDTO
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsSelected { get; set; }
    }
}
