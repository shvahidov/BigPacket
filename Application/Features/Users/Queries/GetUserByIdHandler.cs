using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUserRepository _repo;

    public GetUserByIdHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}
