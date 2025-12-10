using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands;

public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IUserRepository _users;
    private readonly IRoleRepository _roles;
    private readonly PasswordHasher<User> _hasher = new();

    public RegisterHandler(IUserRepository users, IRoleRepository roles)
    {
        _users = users;
        _roles = roles;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var role = await _roles.GetByNameAsync("User");
        if (role == null)
        {
            return "Роль 'User' не найдена";
        }

        var user = new User
        {
            UserId = Guid.NewGuid(),
            UserName = dto.UserName,
            LoginName = dto.LoginName,
            Password = _hasher.HashPassword(
                null!,
                dto.Password),
            PhoneNumber = dto.PhoneNumber,
            Info = dto.Info,
            RoleId = role.RoleId,
            Role = null,
        };

        await _users.AddAsync(user);

        return "Пользователь зарегистрирован";
    }
}