using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class UserResultDto
    {
        public int TotalQuestions { get; set; }
        public int AttemptedQuestions { get; set; }
        public int UnAttemptedQuestions { get; set; }
        public int CorrectQuestions { get; set; }
        public int InCorrectQuestions { get; set; }
        public decimal PercentageObtained { get; set; }
        public string SubjectName { get; set; }

    }
}
