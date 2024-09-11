using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Please enter the Subject name.")]
        public string SubjectName { get; set; }

        //collection property  (One to many)  subject 1 ----> Question *
        public ICollection<Question> Questions { get; set; }
        ICollection<Test> Tests { get; set; } // Subject 1 --> Test * 
    }
}
