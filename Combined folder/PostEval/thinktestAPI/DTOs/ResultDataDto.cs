using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class ResultDataDto
    {
        public string SubjectName { get; set; }
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public int TotalQuestions { get; set; }
        public int AttemptedQuestions { get; set; }
        public int UnAttemptedQuestions { get; set; }
        public int CorrectQuestions { get; set; }
        public int InCorrectQuestions { get; set; }
        public decimal PercentageObtained { get; set; }

    }
}
