using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class ExamResultDto
    {
        public int TotalQuestions { get; set; }
        public int Attempted { get; set; }
        public int Unattempted { get; set; }
        public int Correct { get; set; }
        public int Incorrect { get; set; }
        public double Percentage { get; set; }
    }
}
