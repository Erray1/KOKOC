using System.ComponentModel.DataAnnotations;

namespace KOKOC.ReverseProxy.Persistence.Contracts
{
    public class RegisterRequest
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(30, ErrorMessage = "Длина имени пользователя не должна превышать 30 символов")]
        public string UserName { get; set; }
        public string Password { get; set; }    
    }
}
