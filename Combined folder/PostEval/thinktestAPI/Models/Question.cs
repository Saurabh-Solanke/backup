using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Knowledge__Test_Backend.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Please enter the Question text")]
        public string QuestionText { get; set; }

        // many to one Question * --> subject 1 
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public ICollection<Option> Options { get; set; } // collection property 
        // question 1 --> Options *

        [ForeignKey("QuestionBank")]
        public int QuestionBankId { get; set; }

        public QuestionBank QuestionBank { get; set; }

    }
}
