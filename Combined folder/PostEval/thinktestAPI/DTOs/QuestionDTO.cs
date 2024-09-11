using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int []Answer { get; set; } // Index of the correct option

        public bool IsMultiple { get; set; }
    }
}
