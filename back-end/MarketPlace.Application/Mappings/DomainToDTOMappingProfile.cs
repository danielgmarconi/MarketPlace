using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Entities;

namespace MarketPlace.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
    }
}
