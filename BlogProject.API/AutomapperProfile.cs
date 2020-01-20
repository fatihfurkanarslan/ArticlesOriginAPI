using AutoMapper;
using Entities;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.API
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDetailModel>();
            CreateMap<UserRegisterModel, User>();
            CreateMap<CategoryInsertModel, Category>()
            .ForMember(x=> x.OnModified, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(x => x.OnModifiedUsername, opt => opt.MapFrom(src => "admin"));
            CreateMap<NoteInsertModel, Note>()
            .ForMember(x => x.OnModified, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(x => x.OnModifiedUsername, opt => opt.MapFrom(src => "admin"));
            CreateMap<CommentInsertModel, Comment>()
            .ForMember(x => x.OnModified, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(x => x.OnModifiedUsername, opt => opt.MapFrom(src => "admin"));
               CreateMap<PhotoForCreationModel, Photo>();
                    CreateMap<Photo, PhotoForCreationModel>()
                    .ForMember(x => x.File, opt => opt.Ignore());

            //CreateMap<Category, CategoryInsertModel>();
        }
     
    }
}
