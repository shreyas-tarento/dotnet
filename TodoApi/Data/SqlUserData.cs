using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.Models.SuccessModels;
using TodoApi.SqlDbContext;

namespace TodoApi.Data
{
    public class SqlUserData : IUser
    {
        private readonly GlobalDbContext _context;
        public SqlUserData(GlobalDbContext context)
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

            await _context.User.AddAsync(userInfo);
            await _context.SaveChangesAsync();

            var newUserInfo = _context.User.FirstOrDefault(x => x.UserName == userDetails.UserName);

            var userAddress = new Address()
            {
                UserId = newUserInfo.Id,
                HouseNumber = userDetails.HouseNumber,
                StreetName = userDetails.StreetName,
                City = userDetails.City,
                Town = userDetails.Town,
                Pincode = userDetails.Pincode,
                State = userDetails.State,
                AddressType = userDetails.AddressType,
            };

            await _context.Address.AddAsync(userAddress);
            await _context.SaveChangesAsync();

            var userContact = new Contact()
            {
                UserId = newUserInfo.Id,
                Number = userDetails.Number,
            };

            await _context.Contact.AddAsync(userContact);
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
                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();
            }

            if (address != null)
            {
                _context.Address.Remove(address);
                await _context.SaveChangesAsync();
            }

            _context.User.Remove(userInfo);
            await _context.SaveChangesAsync();

            

           

            return new UserDetailsSuccess()
            {
                Message = $"User for {id} succesfully deleted",
                UserId = id,
            };
        }

        public List<UserDetails> GetAllUsers()
        {
            var usersList = (from userInfo in _context.User
                             join userAddress in _context.Address
                             on userInfo.Id equals userAddress.UserId
                             join userContact in _context.Contact
                             on userInfo.Id equals userContact.UserId
                             select new UserDetails()
                             {
                                 Id = userInfo.Id,
                                 UserId = userInfo.Id,
                                 FirstName = userInfo.FirstName,
                                 LastName = userInfo.LastName,
                                 UserName = userInfo.UserName,
                                 StreetName = userAddress.StreetName,
                                 City = userAddress.City,
                                 Town = userAddress.Town,
                                 AddressType = userAddress.AddressType,
                                 HouseNumber = userAddress.HouseNumber,
                                 Pincode = userAddress.Pincode,
                                 State = userAddress.State,
                                 Number = userContact.Number,
                             }).ToList();

            return usersList;
        }

        public async Task<UserDetails> GetUser(int id)
        {
            var userInfo = await GetUserInfo(id);
            var address = await GetUserAddress(id);
            var contact = await GetUserContact(id);

            return new UserDetails()
            {
                Id = userInfo.Id,
                UserId = userInfo.Id,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                UserName = userInfo.UserName,
                StreetName = address.StreetName,
                City = address.City,
                Town = address.Town,
                AddressType = address.AddressType,
                HouseNumber = address.HouseNumber,
                Pincode = address.Pincode,
                State = address.State,
                Number = contact.Number,
            };
        }

        public async Task<Address> GetUserAddress(int userId)
        {
            return await _context.Address.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Contact> GetUserContact(int userId)
        {
            return await _context.Contact.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<User> GetUserInfo(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<UserDetails> UpdateUser(int id,UserDetails newUserDetails)
        {
            var userInfo = await GetUserInfo(id);
            var address = await GetUserAddress(id);
            var contact = await GetUserContact(id);

            userInfo.FirstName = newUserDetails.FirstName;
            userInfo.LastName = newUserDetails.LastName;
            userInfo.UserName = newUserDetails.UserName;

            address.HouseNumber = newUserDetails.HouseNumber;
            address.StreetName = newUserDetails.StreetName;
            address.City = newUserDetails.City;
            address.Town = newUserDetails.Town;
            address.Pincode = newUserDetails.Pincode;
            address.State = newUserDetails.State;
            address.AddressType = newUserDetails.AddressType;

            contact.Number = newUserDetails.Number;

            await _context.SaveChangesAsync();

            return newUserDetails;
        }

        public async Task<bool> UserExistOnUserName(string userName)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(x => x.UserName == userName);

            if (existingUser != null)
                return true;

            return false;
        }
    }
}
