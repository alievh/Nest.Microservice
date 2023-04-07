﻿using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Nest.Web.Models;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Services;

public class ClientCredentialTokenService : IClientCredentialTokenService
{
    private readonly ServiceApiSettings _serviceApiSettings;
    private readonly ClientSettings _clientSettings;
    private readonly IClientAccessTokenCache _clientAccessTokenCache;
    private readonly HttpClient _httpClient;

    public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient)
    {
        _serviceApiSettings = serviceApiSettings.Value;
        _clientSettings = clientSettings.Value;
        _clientAccessTokenCache = clientAccessTokenCache;
        _httpClient = httpClient;
    }

    public async Task<string> GetToken()
    {
        var currentToken = await _clientAccessTokenCache.GetAsync("WebClient", null);

        if (currentToken != null)
        {
            return currentToken.AccessToken;
        }

        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });

        if (discovery.IsError) throw discovery.Exception;

        var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
        {
            ClientId = _clientSettings.WebClient.ClientId,
            ClientSecret = _clientSettings.WebClient.ClientSecret,
            Address = discovery.TokenEndpoint
        };

        var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

        if (newToken.IsError)
        {
            throw newToken.Exception;
        }

        await _clientAccessTokenCache.SetAsync("WebClient", newToken.AccessToken, newToken.ExpiresIn, null);

        return newToken.AccessToken;
    }
}
