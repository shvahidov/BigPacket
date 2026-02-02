using System.Security.Claims;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _repo;
    private readonly IRoleRepository _roleRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PasswordHasher<User> _hasher = new();

    public CreateUserHandler(IUserRepository repo, IRoleRepository roleRepo, IHttpContextAccessor httpContextAccessor)
    {
        _repo = repo;
        _roleRepo = roleRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var role = await _roleRepo.GetByNameAsync("User");
        if (role == null)
        {
            throw new Exception("Роль 'User' не найдена");
        }

        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid? adminId = string.IsNullOrEmpty(adminIdString) ? null : Guid.Parse(adminIdString);

        var user = new User
        {
            UserId = Guid.NewGuid(),
            UserName = dto.UserName,
            LoginName = dto.LoginName,
            Password = _hasher.HashPassword(null!, dto.Password),
            RoleId = role.RoleId,
            PhoneNumber = dto.PhoneNumber,
            Info = dto.Info,
            ParentId = adminId,
            Role = role,
        };

        await _repo.AddAsync(user);
        return user;
    }
}