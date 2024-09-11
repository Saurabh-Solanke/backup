using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class QuestionAndAnswerDTO
    {
        [Required]
        public int Id { get; set; } // Unique identifier for the question

        [Required(ErrorMessage = "Please enter the question text.")]
        public string Question { get; set; } // The question text

        [Required]
        public List<string> Options { get; set; } // List of options for the question

        [Required]
        public int []Answer { get; set; }

        public bool IsMultiple { get; set; }

    }
}
