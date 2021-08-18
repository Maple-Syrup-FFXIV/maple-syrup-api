using maple_syrup_api.Dto.User;
using maple_syrup_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Services.IService
{
    public interface IUserService
    {
        public Task<UserSummary> Login(string pDiscordCode);

        public Task<UserSummary> Authenticate(User user, string accessToken);

        public void AddPlayer(Player newPlayer, int UserId);

        public void RemovePlayer(int Player, int UserId);

        public List<Player> GetUserPlayerList(int UserId);
        public User GetUser(int UserId);
    }
}
