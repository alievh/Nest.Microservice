using IdentityModel.Client;
using Nest.Shared.DTO_s;
using Nest.Web.Models.Authentication;

namespace Nest.Web.Services.Interfaces;

public interface IIdentityService
{
    Task<ResponseDto<bool>> SignIn(SignInInput signInInput);
    Task<ResponseDto<bool>> Register(RegisterInput registerInput);
    Task<TokenResponse> GetAccessTokenByRefreshToken();
    Task RevokeRefreshToken();
}
