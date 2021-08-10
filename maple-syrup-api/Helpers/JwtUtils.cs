using maple_syrup_api.Dto;
using maple_syrup_api.Exceptions;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace maple_syrup_api.Helpers
{
    public interface IJwtUtils
    {
        public Task<ValidateToken> ValidateJwtToken(string pToken);
    }

    public class JwtUtils : IJwtUtils
    {
        private readonly HttpClient _http = new HttpClient();
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IConfiguration _cfg;
        public JwtUtils(IUserTokenRepository pUserTokenRepository, IConfiguration pConfiguration)
        {
            _userTokenRepository = pUserTokenRepository;
            _cfg = pConfiguration;
        }

        private async Task<string> RefreshJwtToken(UserToken pUserToken)
        {
            UserToken token = pUserToken;
            if (token == null)
                throw new MapleException("Invalid Token");


            Dictionary<string, string> values = new Dictionary<string, string>()
                {
                    {"client_id",_cfg.GetSection("AuthSettings")["ClientId"] },
                    {"client_secret", _cfg.GetSection("AuthSettings")["ClientSecret"] },
                    {"grant_type","refresh_token" },
                    {"refresh_token",token.RefreshToken}
                };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://discord.com/api/oauth2/token")
            {
                Content = new FormUrlEncodedContent(values)
            };
            HttpResponseMessage response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new MapleException("Request to discord servers failed");

            JObject jsonResponse = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            string accessToken = jsonResponse.GetValue("access_token").ToString();
            string refreshToken = jsonResponse.GetValue("refresh_token").ToString();
            double TTL = (double)jsonResponse.GetValue("expires_in");

            token.RefreshToken = refreshToken;
            token.Token = accessToken;
            token.ExpirationDate = DateTime.UtcNow.AddMinutes(TTL);
            _userTokenRepository.AddOrUpdate(token);

            await _userTokenRepository.SaveAsync();

            return accessToken;
        }

        public async Task<ValidateToken> ValidateJwtToken(string pToken)
        {
            UserToken userToken = _userTokenRepository.GetByAccessToken(pToken);
            if (userToken == null)
                return null;

            //if (userToken.ExpirationDate < DateTime.UtcNow)
                await RefreshJwtToken(userToken);

            return new ValidateToken()
            {
                AccessToken = userToken.Token,
                UserId = userToken.UserId
            };
        }


    }
}