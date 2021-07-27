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

        public UserDetailsSuccess CreateUser(UserDetails userDetails)
        {
            var userInfo = new User()
            {
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                UserName = userDetails.UserName
            };

            _context.User.Add(userInfo);
            _context.SaveChanges();

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

            _context.Address.Add(userAddress);
            _context.SaveChanges();

            var userContact = new Contact()
            {
                UserId = newUserInfo.Id,
                Number = userDetails.Number,
            };

            _context.Contact.Add(userContact);
            _context.SaveChanges();

            return new UserDetailsSuccess()
            {
                Message = $"New user create #{newUserInfo.Id}",
                UserId = newUserInfo.Id
            };
        }

        public UserDetailsSuccess DeleteUser(int id)
        {
            var userInfo = GetUserInfo(id);
            var address = GetUserAddress(id);
            var contact = GetUserContact(id);

            if (contact != null)
            {
                _context.Contact.Remove(contact);
                _context.SaveChanges();
            }

            if (address != null)
            {
                _context.Address.Remove(address);
                _context.SaveChanges();
            }

            _context.User.Remove(userInfo);
            _context.SaveChanges();

            

           

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

        public UserDetails GetUser(int id)
        {
            var userInfo = GetUserInfo(id);
            var address = GetUserAddress(id);
            var contact = GetUserContact(id);

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

        public Address GetUserAddress(int userId)
        {
            return _context.Address.FirstOrDefault(x => x.UserId == userId);
        }

        public Contact GetUserContact(int userId)
        {
            return _context.Contact.FirstOrDefault(x => x.UserId == userId);
        }

        public User GetUserInfo(int userId)
        {
            return _context.User.FirstOrDefault(x => x.Id == userId);
        }

        public UserDetails UpdateUser(int id, UserDetails newUserDetails)
        {
            var userInfo = GetUserInfo(id);
            var address = GetUserAddress(id);
            var contact = GetUserContact(id);

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
            _context.User.Update(userInfo);
            _context.SaveChanges();

            if (address != null)
            {
                _context.Address.Update(address);
                _context.SaveChanges();
            }
            else
            {
                address.UserId = userInfo.Id;
                _context.Address.Add(address);
                _context.SaveChanges();
            }

            if (address != null)
            {
                _context.Contact.Update(contact);
                _context.SaveChanges();
            }
            else
            {
                contact.UserId = userInfo.Id;
                _context.Contact.Add(contact);
                _context.SaveChanges();
            }

            return newUserDetails;
        }

        public bool UserExistOnUserName(string userName)
        {
            var existingUser = _context.User.FirstOrDefault(x => x.UserName == userName);

            if (existingUser != null)
                return true;

            return false;
        }
    }
}
