using maple_syrup_api.Dto;
using maple_syrup_api.Dto.User;
using maple_syrup_api.Exceptions;
using maple_syrup_api.Helpers;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Services.IService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace maple_syrup_api.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _cfg;
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IConfiguration cfg, IUserRepository pUserRepository, IUserTokenRepository pUserTokenRepository, IJwtUtils pJwtUtils)
        {
            _cfg = cfg;
            _userRepository = pUserRepository;
            _userTokenRepository = pUserTokenRepository;
            _jwtUtils = pJwtUtils;
        }

        #region Auth

        public async Task<UserSummary> Login(string pDiscordCode)
        {
            using (HttpClient http = new HttpClient())
            {
                Dictionary<string, string> values = new Dictionary<string, string>()
                {
                    {"client_id", _cfg.GetSection("AuthSettings")["ClientId"] },
                    {"client_secret", _cfg.GetSection("AuthSettings")["ClientSecret"] },
                    {"grant_type", "authorization_code" },
                    {"code", pDiscordCode},
                    {"redirect_uri", "http://localhost:4200/auth" }
                };
                var request = new HttpRequestMessage(HttpMethod.Post, "https://discord.com/api/oauth2/token")
                {
                    Content = new FormUrlEncodedContent(values)
                };

                HttpResponseMessage response = await http.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    JObject jsonResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
                    string accessToken = jsonResponse.GetValue("access_token").ToString();
                    string refreshToken = jsonResponse.GetValue("refresh_token").ToString();
                    double TTL = (double)jsonResponse.GetValue("expires_in");


                    http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jsonResponse.GetValue("access_token").ToString());
                    HttpResponseMessage response2 = await http.GetAsync("https://discord.com/api/users/@me");
                    if (response2.IsSuccessStatusCode)
                    {
                        jsonResponse = JObject.Parse(await response2.Content.ReadAsStringAsync());
                        long discordId = (long)jsonResponse.GetValue("id");
                        User user = _userRepository.GetUserByDiscordId(discordId);
                        if (user == null)
                        {
                            user = new User()
                            {
                                DiscordId = discordId,
                                Image = "https://cdn.discordapp.com/" + discordId + "/" + jsonResponse.GetValue("avatar").ToString(),
                                UserName = jsonResponse.GetValue("username").ToString()
                            };
                            _userRepository.InsertAndGetId(user);
                        }

                        UserToken token = new UserToken()
                        {
                            User = user,
                            UserId = user.Id,
                            ExpirationDate = DateTime.UtcNow.AddMinutes(TTL),
                            Token = accessToken,
                            RefreshToken = refreshToken
                        };

                        _userTokenRepository.Add(token);

                        await _userTokenRepository.SaveAsync();

                        return new UserSummary()
                        {
                            Id = user.Id,
                            Image = user.Image,
                            Username = user.UserName,
                            AccessToken = accessToken
                        };

                    }
                }
                throw new MapleException("OWO");

            }
        }

        public async Task<UserSummary> Authenticate(User user, string accessToken)
        {
            if (user == null)
                throw new UnauthorizedException("Invalid Token");

            ValidateToken validateToken = await _jwtUtils.ValidateJwtToken(accessToken);

            if (!validateToken.UserId.HasValue)
                throw new UnauthorizedException();



            return new UserSummary()
            {
                Id = user.Id,
                Image = user.Image,
                AccessToken = validateToken.AccessToken,
                Username = user.UserName
            };
        }




        #endregion
    }
}
