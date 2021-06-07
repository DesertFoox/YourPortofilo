using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Mapping;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface IUserRepo
    {
        public List<AllUserDTO> GetAllUsers();
        public User GetUserById(Guid id);
        public UserProfile UpdateUser(Guid id,UserProfile NewUserInf);
        public void DeleteUserByid(User user);
        public void Register(User user);
        public User Login(User user);
        public void BanUser(User user);
        public void SetUserAdmin(Guid id);
        public void VerifyUser(Guid id);
        public User GetUserByEmailOrUserName(string Email,string Username);
        public User GetUserByEmailOrUserNameLogin(string Email);
        public void ChangePassword(Guid id,User user);
        public void UploadUserImage(Guid Id,string image);
        object GetUserById(Attribute[] attributes);
        public UserProfile GetProfileByUserId(Guid id);
        public void CreateUserProfile(UserProfile user);
        public User GetUserNameById(Guid id);
        public void DeleteHardUser(User user);
    }
}
