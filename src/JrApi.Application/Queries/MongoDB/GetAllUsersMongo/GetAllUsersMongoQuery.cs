using JrApi.Domain.Entities.Users;
using MediatR;

namespace JrApi.Application.Queries.MongoDB.GetAllUsersMongo
{
    public sealed class GetAllUsersMongoQuery : IRequest<IEnumerable<User>>
    {
        
    }
}
