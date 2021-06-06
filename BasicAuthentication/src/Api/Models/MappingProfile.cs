using Api.Models.Requests;
using Api.Models.Responses;
using AutoMapper;
using Core.Entities;
using Core.Extensions;
using Core.Services;
using System;

namespace Api.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile(IPasswordService passwordService)
        {
            string timezoneId = "India Standard Time";

            // Requests
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, options => options.MapFrom(src => src.LastName))
                .ForMember(dest => dest.DateOfBirth, options => options.MapFrom(src => src.DateOfBirth.ToUniversalTime()))
                .ForMember(dest => dest.PhoneNo, options => options.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Username, options => options.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, options => options.MapFrom(src => passwordService.HashPassword(src.Password)))
                .ForMember(dest => dest.CreatedAt, options => options.MapFrom(_ => DateTime.UtcNow));

            // Responses
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, options => options.MapFrom(src => src.LastName))
                .ForMember(dest => dest.DateOfBirth, options => options.MapFrom(src => src.DateOfBirth.FromUTCToLocal(timezoneId)))
                .ForMember(dest => dest.Phone, options => options.MapFrom(src => src.PhoneNo))
                .ForMember(dest => dest.Username, options => options.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedAt, options => options.MapFrom(src => src.CreatedAt.FromUTCToLocal(timezoneId)))
                .ForMember(dest => dest.UpdatedAt, options => options.MapFrom(src => src.UpdatedAt.HasValue
                                ? src.UpdatedAt.Value.FromUTCToLocal(timezoneId)
                                : src.UpdatedAt.GetValueOrDefault()));
        }
    }
}
