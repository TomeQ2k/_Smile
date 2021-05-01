using MediatR;
using Smile.Core.Application.Logic.Responses.Query.Profile;

namespace Smile.Core.Application.Logic.Requests.Query.Profile
{
    public class RefreshUserDataRequest : IRequest<RefreshUserDataResponse>
    { }
}