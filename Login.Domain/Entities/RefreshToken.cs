using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime AbsoluteExpireDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

}
