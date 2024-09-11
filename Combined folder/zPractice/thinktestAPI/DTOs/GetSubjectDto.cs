using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class GetSubjectDto
    {
        public int SubjectId { get; set; }

        public string SubjectName { get; set; }
    }
}
