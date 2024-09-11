using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Knowledge__Test_Backend.DTOs
{
    public class LoggedInUser
    {
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public string UserFullname { get; set; }
        public int UserId { get; set; }

        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public string Role { get; set; }
    }
}
