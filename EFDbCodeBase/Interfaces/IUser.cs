using EFDbCodeBase.Models;
using EFDbCodeBase.Models.SuccessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDbCodeBase.Interfaces
{
    public interface IUser
    {
        List<UserDetails> GetAllUsers();
        Task<UserDetails> GetUser(int id);
        Task<UserDetailsSuccess> CreateUser(UserDetails userDetails);
        Task<UserDetails> UpdateUser(int id, UserDetails newUserDetails);
        Task<UserDetailsSuccess> DeleteUser(int id);

        Task<Address> GetUserAddress(int userId);
        Task<User> GetUserInfo(int userId);
        Task<Contact> GetUserContact(int userId);
        Task<bool> UserExistOnUserName(string userName);
    }
}
