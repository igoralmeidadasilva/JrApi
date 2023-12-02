using System;
using MediatR;

namespace JrApi.Application.Commands.UserMongo.DeleteUserMongo
{
    public sealed class DeleteUserMongoCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
