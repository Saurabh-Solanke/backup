using Microsoft.AspNetCore.Mvc.Formatters;

namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class QuestionGetDTO
    {
        public int QuestionId { get; set; }        // Question identifier
        public string QuestionText { get; set; }   // The question text
        public bool IsMultiple { get; set; }       // Indicates if the question allows multiple answers
        public DateTime CreatedDate { get; set; }  // Date the question was created
        public bool HasDifferentialMarking { get; set; }
        public MediaType mediaType { get; set; }
        public string MediaUrl { get; set; }

        public List<OptionGetDTO> Options { get; set; }   // List of options for the question
    }
}
