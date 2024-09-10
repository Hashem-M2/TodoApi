using AutoMapper;
using DTOS;
using DTOS.TodoDto;
using DTOS.UserDtos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TodoItem, TodoDto>()
                         .ReverseMap();
            

            CreateMap<TodoUpdateDto, TodoItem>()
                 .ReverseMap();


            CreateMap<TodoCreateDto, TodoItem>()
                .ReverseMap();






            CreateMap<RegisterDto, User>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<LoginDto, User>().ReverseMap();


        }
    }
}
