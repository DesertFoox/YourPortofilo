using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Mapping;

namespace Application.Interfaces
{
    public class UserSql : IUserRepo
    {
        private readonly IAppDBContext _context;
        public UserSql(IAppDBContext context)
        {
            _context = context;
        }
        public void BanUser(User user)
        {
            user.IsBanned = !user.IsBanned;
        }

        public void ChangePassword(Guid id, User user)
        {
            var User = GetUserById(id);
            User.Password = user.Password;
        }
        public List<AllUserDTO> GetAllUsers()
        {
            return _context.Users.Select(x => new AllUserDTO
            {
                Email = x.Email,
                Id = x.Id,
                IsActive = x.IsActive,
                Role = x.Role,
                IsBanned = x.IsBanned,
                IsDeleted = x.IsDeleted,
                UserName = x.UserName,
            }).ToList();
        }

        public User GetUserById(Guid id)
        {
            return _context.Users.Include(x => x.UserBlogs).Include(x => x.UserSkills).Include(x => x.PortoFiles).Include(x => x.UserProfile).FirstOrDefault(x => x.Id == id);
        }

        public User Login(User user)
        {
            var User = _context.Users.FirstOrDefault(e => e.Email == user.Email || e.UserName == user.Email && e.Password == user.Password);
            return User;
        }

        public void Register(User user)
        {
            _context.Users.Add(user);
            user.Role = "User";
            user.CreateDate = DateTime.Now;
            UserProfile profile = new UserProfile();
            profile.UserId = user.Id;
            _context.UserProfile.Add(profile);
            SaveChanges();
        }

        public void SetUserAdmin(Guid id)
        {
            var User = GetUserById(id);
            User.Role = "Admin";
            _context.SaveChanges();

        }

        public void VerifyUser(Guid id)
        {
            var User = GetUserById(id);
            User.IsActive = true;
            SaveChanges();

        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteUserByid(User user)
        {
            user.IsDeleted = true;

            SaveChanges();
        }
        public UserProfile UpdateUser(Guid id, UserProfile NewUserInf)
        {
            var User = GetProfileByUserId(id);
            User.City = NewUserInf.City;
            User.FirstName = NewUserInf.FirstName;
            User.Image = NewUserInf.Image;
            User.LastName = NewUserInf.LastName;
            User.PhoneNumber = NewUserInf.PhoneNumber;
            User.TelegramId = NewUserInf.TelegramId;
            User.BirthDate = NewUserInf.BirthDate;
            User.TwitterLink = NewUserInf.TwitterLink;
            User.Instagram = NewUserInf.Instagram;
            SaveChanges();
            return User;
        }

        public User GetUserByEmailOrUserName(string Email,string Username)
        {
            return _context.Users.FirstOrDefault(x => x.Email == Email || x.UserName == Username);
        }

        public void UploadUserImage(Guid Id, string image)
        {
            var User = GetProfileByUserId(Id);
            User.Image = image;
            SaveChanges();
        }

        public UserProfile GetProfileByUserId(Guid id)
        {
            return _context.UserProfile.FirstOrDefault(x => x.UserId == id);
        }

        public void CreateUserProfile(UserProfile user)
        {
            _context.UserProfile.Add(user);
            SaveChanges();
        }

        public object GetUserById(Attribute[] attributes)
        {
            throw new NotImplementedException();
        }

        public User GetUserNameById(Guid id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByEmailOrUserNameLogin(string Email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == Email || x.UserName == Email);
        }

        public void DeleteHardUser(User user)
        {
            _context.Users.Remove(user);
            SaveChanges();
        }
    }
}
