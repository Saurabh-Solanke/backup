using System.ComponentModel.DataAnnotations.Schema;

namespace MCQExamApi.Dtos.Exam
{
    public class ExamRespDTO
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Duration { get; set; }
        public int? TotalMarks { get; set; }
        public int? PassingMarks { get; set; }
        public bool IsActive { get; set; }
        public ICollection<QuestionRespDTO> Questions { get; set; }
    }

    public class QuestionRespDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int? Points { get; set; }
        public ICollection<OptionRespDTO> Options { get; set; }
    }

    public class OptionRespDTO
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
    }

}
