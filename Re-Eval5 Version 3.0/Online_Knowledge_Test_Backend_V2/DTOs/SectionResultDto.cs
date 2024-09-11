namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class SectionResultDto
    {
        public int SectionId { get; set; }
        public int AttemptedQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double SectionScore { get; set; }  // Calculated score for the section
        public bool IsPassed { get; set; }  // Whether the user passed the section or not
    }
}
