using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<User>>
{
    private readonly IUserRepository _repo;

    public GetAllUsersHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return _repo.GetAllAsync();
    }
}