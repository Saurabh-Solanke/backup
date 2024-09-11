using Microsoft.AspNetCore.Mvc.Formatters;

namespace Online_Knowledge_Test_Backend_V2.DTOs
{
    public class CreateQuestionDto
    {
        public int SectionId { get; set; }
        public string QuestionText { get; set; }
        public bool IsMultipleChoice { get; set; }
        public bool HasDifferentialMarking { get; set; }
        public MediaType mediaType { get; set; }
        public string MediaUrl { get; set; }
        public List<CreateOptionDto> Options { get; set; }
    }
}
