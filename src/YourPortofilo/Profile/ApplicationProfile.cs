using Application.Mapping;
using Domain.Entities;
using AutoMapper;
using Application.Mapping.Portofilo;
using Application;

namespace Application.Profiles
{
    class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {

            //user entity
            CreateMap<AllUserDTO, User>();
            CreateMap<User, UserByIdDTO>();
            CreateMap<UserByIdDTO, User>();
            CreateMap<User, AllUserDTO>();
            CreateMap<User, LoginUserDTO>();
            CreateMap<LoginUserDTO, User>();
            CreateMap<UploadImageUserDTO, UserProfile>();
            CreateMap<UserProfile, UploadImageUserDTO>();
            CreateMap<UserProfileCreateDTO, UserProfile>();
            CreateMap<UserProfile, UserProfileCreateDTO>();
            CreateMap<UpdateProfileDTO, UserProfile>();
            CreateMap<UserProfile, UpdateProfileDTO>();
            CreateMap<User, RegisterUserDTO>();
            CreateMap<RegisterUserDTO, User>();
            CreateMap<UserNameDTO, User>();
            CreateMap<User, UserNameDTO>();

            //skill entity
            CreateMap<Skill,CreateSkillDTo>();
            CreateMap<CreateSkillDTo, Skill>();
            CreateMap<UpdateSkillDTO,Skill>();
            CreateMap<Skill,UpdateSkillDTO >();

            //portofilo
            CreateMap<PortoFilo,CreatePortoDTO>();
            CreateMap<CreatePortoDTO,PortoFilo>();
            CreateMap<PortoFilo,UpdatePortofiloDTO>();
            CreateMap<UpdatePortofiloDTO, PortoFilo>();
           
            //Blog
            CreateMap<User, UserBlogViewModel>();
            CreateMap<UserBlogViewModel,User >();
            CreateMap<Blog,UpdateBlogDTO>();
            CreateMap<UpdateBlogDTO, Blog>();
            CreateMap<Blog, CreateBlogDTO>();
            CreateMap<CreateBlogDTO,Blog>();

            //BlogComment
            CreateMap<BlogComment,CreateCommentDTO>();
            CreateMap<CreateCommentDTO, BlogComment>();
            CreateMap<Blog,BlogGetAllComment>();
            CreateMap<BlogGetAllComment, Blog>();
            CreateMap<BlogComment,BlogCommentDTO>();
            CreateMap<BlogCommentDTO, BlogComment>();

        }
    }
}
