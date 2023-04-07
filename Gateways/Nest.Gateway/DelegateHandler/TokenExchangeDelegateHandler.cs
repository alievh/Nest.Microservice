using IdentityModel.Client;

namespace Nest.Gateway.DelegateHandler;

public class TokenExchangeDelegateHandler : DelegatingHandler
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private string _accesToken;

    public TokenExchangeDelegateHandler(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    private async Task<string> GetToken(string requestToken)
    {
        if (string.IsNullOrEmpty(requestToken))
        {
            return _accesToken;
        }

        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _configuration["IdentityServerUrl"],
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });

        if (discovery.IsError) throw discovery.Exception;

        TokenExchangeTokenRequest tokenExchangeTokenRequest = new()
        {
            Address = discovery.TokenEndpoint,
            ClientId = _configuration["ClientId"],
            ClientSecret = _configuration["ClientSecret"],
            GrantType = _configuration["TokenGrantType"],
            SubjectToken = requestToken,
            SubjectTokenType = "urn:ietf:params:oauth:grant-type:access-token",
            Scope = "openid payment_fullpermission discount_fullpermission",
        };

        var tokenResponse = await _httpClient.RequestTokenExchangeTokenAsync(tokenExchangeTokenRequest);

        if (tokenResponse.IsError)
        {
            throw tokenResponse.Exception;
        }

        _accesToken = tokenResponse.AccessToken;

        return _accesToken;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var requestToken = request.Headers.Authorization.Parameter;

        var newToken = await GetToken(requestToken);

        request.SetBearerToken(newToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
