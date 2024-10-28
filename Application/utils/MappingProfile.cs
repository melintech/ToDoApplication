using Application.DTOs;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoItem, ToDoItemDto>().ReverseMap();
            CreateMap<CreateToDoItemCommand, ToDoItem>().ReverseMap();
            CreateMap<UpdateToDoItemCommand, ToDoItem>().ReverseMap();
        }
    }
}
