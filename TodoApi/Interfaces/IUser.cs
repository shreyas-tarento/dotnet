using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Models.SuccessModels;

namespace TodoApi.Interfaces
{
    public interface IUser
    {
        List<UserDetails> GetAllUsers();
        UserDetails GetUser(int id);
        UserDetailsSuccess CreateUser(UserDetails userDetails);
        UserDetails UpdateUser(int id, UserDetails newUserDetails);
        UserDetailsSuccess DeleteUser(int id);

        Address GetUserAddress(int userId);
        User GetUserInfo(int userId);
        Contact GetUserContact(int userId);
        bool UserExistOnUserName(string userName);
    }
}
