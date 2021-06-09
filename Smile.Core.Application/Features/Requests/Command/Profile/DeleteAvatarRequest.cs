using MediatR;
using Smile.Core.Application.Features.Responses.Command.Profile;

namespace Smile.Core.Application.Features.Requests.Command.Profile
{
    public class DeleteAvatarRequest : IRequest<DeleteAvatarResponse>
    { }
}