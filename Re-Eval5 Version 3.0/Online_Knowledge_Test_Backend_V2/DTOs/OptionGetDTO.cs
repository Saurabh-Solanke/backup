namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class OptionGetDTO
    {
        public int OptionId { get; set; }        // Option identifier
        public string OptionText { get; set; }   // Text for the option
        public bool IsCorrect { get; set; }      // Indicates if the option is correct
        public int Marks { get; set; }
    }
}
