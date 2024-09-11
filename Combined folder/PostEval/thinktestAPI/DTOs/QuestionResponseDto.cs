using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class QuestionResponseDto
    {
        public int QuestionId { get; set; }
        public List<int> SelectedOptions { get; set; }
    }
}
