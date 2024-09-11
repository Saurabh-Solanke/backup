using MCQExamApi.Dtos.Option;

namespace MCQExamApi.Dtos.Question
{
    public class QuestionResultDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public decimal PointsScored { get; set; }
        public List<OptionResultDTO> Options { get; set; } = new List<OptionResultDTO>();

    }
}
