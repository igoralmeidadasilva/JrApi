using JrApi.Domain.Entities.Users;
using MediatR;

namespace JrApi.Application.Queries.MongoDB.GetUserByIdMongo
{
    public sealed class GetUserByIdMongoQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
}
