using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionDTO> Questions { get; set; }

    }
}
