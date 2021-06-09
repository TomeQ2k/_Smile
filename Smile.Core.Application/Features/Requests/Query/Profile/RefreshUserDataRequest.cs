using MediatR;
using Smile.Core.Application.Features.Responses.Query.Profile;

namespace Smile.Core.Application.Features.Requests.Query.Profile
{
    public class RefreshUserDataRequest : IRequest<RefreshUserDataResponse>
    { }
}