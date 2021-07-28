using EFDbCodeBase.Interfaces;
using EFDbCodeBase.Models;
using EFDbCodeBase.Models.SuccessModels;
using EFDbCodeBase.SqlDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDbCodeBase.data
{
    public class SqlUserData : IUser
    {
        private readonly SqlContext _context;
        public SqlUserData(SqlContext context)
        {
            _context = context;
        }

        public async Task<UserDetailsSuccess> CreateUser(UserDetails userDetails)
        {
            var userInfo = new User()
            {
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                UserName = userDetails.UserName
            };

            await _context.Users.AddAsync(userInfo);
            await _context.SaveChangesAsync();

            var newUserInfo = _context.Users.FirstOrDefault(x => x.UserName == userDetails.UserName);

            var userAddress = new Address()
            {
                UserId = newUserInfo.Id,
                AddressLine1 = userDetails.AddressLine1,
                AddressLine2 = userDetails.AddressLine2,
                Pincode = userDetails.Pincode,
            };

            await _context.Addresses.AddAsync(userAddress);
            await _context.SaveChangesAsync();

            var userContact = new Contact()
            {
                UserId = newUserInfo.Id,
                Mobile = userDetails.Mobile,
                Telephone = userDetails.Telephone,
                Alternative = userDetails.Alternative,
            };

            await _context.Contacts.AddAsync(userContact);
            await _context.SaveChangesAsync();

            return new UserDetailsSuccess()
            {
                Message = $"New user create #{newUserInfo.Id}",
                UserId = newUserInfo.Id
            };
        }

        public async Task<UserDetailsSuccess> DeleteUser(int id)
        {
            var userInfo = await GetUserInfo(id);
            var address = await GetUserAddress(id);
            var contact = await GetUserContact(id);

            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }

            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }

            _context.Users.Remove(userInfo);
            await _context.SaveChangesAsync();

            return new UserDetailsSuccess()
            {
                Message = $"User for {id} succesfully deleted",
                UserId = id,
            };
        }

        public List<UserDetails> GetAllUsers()
        {
            var usersList = (from userInfo in _context.Users
                             join userAddress in _context.Addresses
                             on userInfo.Id equals userAddress.UserId
                             join userContact in _context.Contacts
                             on userInfo.Id equals userContact.UserId
                             select new UserDetails()
                             {
                                 Id = userInfo.Id,
                                 UserId = userInfo.Id,
                                 FirstName = userInfo.FirstName,
                                 LastName = userInfo.LastName,
                                 UserName = userInfo.UserName,
                                 AddressLine1 = userAddress.AddressLine1,
                                 AddressLine2 = userAddress.AddressLine2,
                                 Pincode = userAddress.Pincode,
                                 Mobile = userContact.Mobile,
                                 Telephone = userContact.Telephone,
                                 Alternative = userContact.Alternative,
                             }).ToList();

            return usersList;
        }

        public async Task<UserDetails> GetUser(int id)
        {
            var userInfo = await GetUserInfo(id);
            var userAddress = await GetUserAddress(id);
            var userContact = await GetUserContact(id);

            return new UserDetails()
            {
                Id = userInfo.Id,
                UserId = userInfo.Id,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                UserName = userInfo.UserName,
                AddressLine1 = userAddress.AddressLine1,
                AddressLine2 = userAddress.AddressLine2,
                Pincode = userAddress.Pincode,
                Mobile = userContact.Mobile,
                Telephone = userContact.Telephone,
                Alternative = userContact.Alternative,
            };
        }

        public async Task<Address> GetUserAddress(int userId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Contact> GetUserContact(int userId)
        {
            return await _context.Contacts.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<User> GetUserInfo(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<UserDetails> UpdateUser(int id, UserDetails newUserDetails)
        {
            var userInfo = await GetUserInfo(id);
            var address = await GetUserAddress(id);
            var contact = await GetUserContact(id);

            userInfo.FirstName = newUserDetails.FirstName;
            userInfo.LastName = newUserDetails.LastName;
            userInfo.UserName = newUserDetails.UserName;

            address.AddressLine1 = newUserDetails.AddressLine1;
            address.AddressLine2 = newUserDetails.AddressLine2;
            address.Pincode = newUserDetails.Pincode;

            contact.Mobile = newUserDetails.Mobile;
            contact.Telephone = newUserDetails.Telephone;
            contact.Alternative = newUserDetails.Alternative;

            await _context.SaveChangesAsync();

            return newUserDetails;
        }

        public async Task<bool> UserExistOnUserName(string userName)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            if (existingUser != null)
                return true;

            return false;
        }
    }
}

