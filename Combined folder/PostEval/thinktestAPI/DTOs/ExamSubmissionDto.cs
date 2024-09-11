using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class ExamSubmissionDto
    {
        public List<QuestionResponseDto> Responses { get; set; }
    }
}
