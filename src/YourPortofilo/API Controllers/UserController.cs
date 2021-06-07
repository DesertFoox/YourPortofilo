using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Mapping;
using AutoMapper;
using System.IO;
using Infranstructure.Services;
using Microsoft.AspNetCore.Authorization;

namespace YourPortofilo.API_Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;
        public UserController(IUserRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<AllUserDTO>> GetAllUsers()
        {
            var Users = _repository.GetAllUsers();
            return Ok(new { StatusCode = 200, Users = Users }); ;
        }
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult GetUserById(Guid id)
        {
            var User = _repository.GetUserById(id);
            if (User is null) return BadRequest(new { statuscode = 400, errormessage = "user with this id is null" });
            var UserModel = _mapper.Map<UserByIdDTO>(User);
            UserModel.UserProfile.User = null;
            return Ok(new { statuscode = 200, User = UserModel });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteUserById(Guid id)
        {
            var User = _repository.GetUserById(id);
            if (User is null) return BadRequest(new { statuscode = 400, errormessage = "user with this id is null" });
            _repository.DeleteUserByid(User);
            return Ok(new { statuscode = 200, message = "User Deleted Successfully" });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHardUser(Guid id)
        {
            var User = _repository.GetUserById(id);
            if (User is null) return NotFound(new { statuscode = 404, errormessage = "user with this id is null" });
            _repository.DeleteHardUser(User);
            return Ok(new { statuscode = 200, message = "User Deleted Successfully" });
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public ActionResult VerifyUserById(Guid id)
        {
            var User = _repository.GetUserById(id);
            if (User is null) return BadRequest(new { statuscode = 400, errormessage = "user with this id is null" });
            _repository.VerifyUser(User.Id);
            return Ok(new { statuscode = 200, message = "User Actived Successfully" });
        }
        [Authorize(Roles = "Admin")]

        [HttpPatch("{id}")]
        public ActionResult SetUserAdmin(Guid id)
        {
            var User = _repository.GetUserById(id);
            if (User is null) return BadRequest(new { statuscode = 400, errormessage = "user with this id is null" });
            _repository.SetUserAdmin(User.Id);
            return Ok(new { statuscode = 200, message = "User Set As Admin Successfully" });

        }
        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public ActionResult UpdateUser(Guid id, UpdateProfileDTO user)
        {
            var User = _repository.GetProfileByUserId(id);
            if (User is null) return NotFound(new { statuscode = 404, errormessage = "user with this id is null" });
            _repository.UpdateUser(id, _mapper.Map<UserProfile>(user));
            return Ok(new { statuscode = 200, message = "User Updated Successfully" });
        }
        [Authorize()]

        [HttpPost("{id}")]
        public ActionResult UploadUserImage([FromForm] UploadImageUserDTO img, Guid id)
        {
            var dir = Directory.GetCurrentDirectory() + "\\wwwroot\\";

            if (img.Image.Length > 0)
            {
                if (img.Image.FileName.EndsWith(".png") || img.Image.FileName.EndsWith(".jpg") || img.Image.FileName.EndsWith(".jpeg"))
                {
                    var NewFileName = img.Image.FileName;
                    Random RandomNumberName = new Random();
                    NewFileName = RandomNumberName.Next(1000, 10000) + "_" + img.Image.FileName;
                    if (!Directory.Exists(dir + "\\uploads\\UserPicture\\"))
                    {
                        Directory.CreateDirectory(dir + "\\uploads\\UserPicture\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(dir + "\\uploads\\UserPicture\\" + NewFileName))
                    {

                        img.Image.CopyTo(filestream);
                        filestream.Flush();
                        _repository.UploadUserImage(id, NewFileName);
                        return Ok(new { statuscode = 200, message = "Image Uploaded Successfully" });
                    }
                }
                else
                {
                    return BadRequest(new { Statuscode = 415, Message = "UnsupportedContent,Suppoerted format is jpg png jpeg" });
                }

            }
            else
            {
                return BadRequest(new { statuscode = 400, errormessage = "Something Went Wront Please Try Again" });
            }
        }


        [HttpPost]
        public ActionResult Register(RegisterUserDTO user)
        {
            if (_repository.GetUserByEmailOrUserName(user.Email, user.UserName) != null) return BadRequest(new { statuscode = 400, message = "Usert With this Information are Avaible,please Use Other Information" });
            _repository.Register(_mapper.Map<User>(user));
            var User = _repository.GetUserByEmailOrUserName(user.Email, user.UserName);
            var UserRegisterRoute = "<a href='https://salarnili.ir/verifyEmail/" + User.Id + "'>" + "فعال سازی حساب کاربری" + "</a>";
            SendEmailVerify.Send(user.Email, "حساب کاربری خود را فعال کنید", "<h1>سلام کاربر عزیز</h1 " + "<br></br>" + "<p>با درود فراوان خدمت شما متشکر از ثبت نام شما،برای فعال سازی حساب  کاربری خود بر روی این لینک کلیم کنید</p>" + UserRegisterRoute);
            return Ok(new { statuscode = 200, message = "Register Successfully,Now You Should Active Your Account,Check Your Email" });
        }

        [HttpPost]
        public ActionResult<LoginUserDTO> Login(LoginUserDTO user)
        {
            string key = "thisissecretKEyfromiranianguyirani!@#goddamnhackerfuckub!tche$";
            var issuer = "http://localhost:5000";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var UserInformation = _repository.GetUserByEmailOrUserNameLogin(user.Email);
            //Create a List of Claims, Keep claims name short    
            if (UserInformation is null)
            {
                return BadRequest(new { statuscode = 400, message = "ایمیل یا رمز عبور شما اشتباه است" });
            }
            if (UserInformation.IsActive is false)
            {
                return BadRequest(new { statuscode = 400, message = "your account is not verify,please verify it from your email inbox" });
            }
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("Id", UserInformation.Id.ToString()));
            permClaims.Add(new Claim("Username", UserInformation.UserName));
            permClaims.Add(new Claim("role", UserInformation.Role));



            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddHours(2),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            if (UserInformation is null) return BadRequest(new { statuscode = 400, message = "The information is wrong" });
            var UserModel = _mapper.Map<User>(user);
            var User = _repository.Login(UserModel);
            return Ok(new { statuscode = 200, message = "Wellcom" + " " + User.UserName, jwttoken = jwt_token });
        }
    
        [Authorize()]

        [HttpGet("{id}")]
        public IActionResult ReturnUserImage(Guid id)
        {
            var User = _repository.GetProfileByUserId(id);
            if (User is null) return NotFound(new { statuscode = 404, message = "User Not Found" });
            if (User.Image is null) return BadRequest(new { statuscode = 400, message = "User Dont have Image" });
            var Image = System.IO.File.OpenRead("wwwroot/uploads/UserPicture/" + User.Image);
            return File(Image, "image/png");
        }
    }
}
