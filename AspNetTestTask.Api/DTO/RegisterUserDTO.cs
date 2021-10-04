using System.ComponentModel.DataAnnotations;


namespace AspNetTestTask.Api.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
