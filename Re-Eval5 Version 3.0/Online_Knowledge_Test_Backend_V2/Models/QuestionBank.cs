using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge_Test_Backend_V2.Models
{
    public class QuestionBank
    {
        [Key]
        public int QuestionBankId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        // Navigation property for the list of questions
        public ICollection<Question> Questions { get; set; } = new List<Question>();

      


    }
}
