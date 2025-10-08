namespace Application.DTOs;

public class CreateUserDto
{
    public string UserName { get; set; }

    public string LoginName { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }

    public string Info { get; set; }
}