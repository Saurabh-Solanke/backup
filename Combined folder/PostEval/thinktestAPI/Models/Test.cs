using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge__Test_Backend.Models
{
    public class Test
    {
        [Key]
        public int TestId { get; set; }
        public string SubjectName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ConductedOn { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        ICollection<Section> Sections { get; set; }


    }
}
