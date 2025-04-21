using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Dtos
{
    public class LoginSuccessDTO
    {
        [Required]
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PlayerId { get; set; } = null!;
        public int PlayerNumId { get; set; }
    }
}
