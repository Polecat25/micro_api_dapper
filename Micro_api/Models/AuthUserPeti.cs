using System.ComponentModel.DataAnnotations;

namespace Micro_api.Models
{
    public class AuthUserPeti
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string pass { get; set; }
    }

    public class AuthUserResp
    {
        public string email { get; set; }
        public string token { get; set; }
    }
}
