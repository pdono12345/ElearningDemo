namespace ElearningDemo.Areas.Admin.ViewModels;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
