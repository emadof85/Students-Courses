using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            // Map from CreateStudentDto to Student.
            CreateMap<CreateStudentDto, Student>()
                .ForMember(dest => dest.Name,
                           opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.Email)) // Assuming Student inherits from ApplicationUser.
                .ForMember(dest => dest.Email,
                           opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)); // Map Address

            // Map from UpdateStudentDto to Student.
            CreateMap<UpdateStudentDto, Student>();

            // Map from Student to StudentDto.
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.Name));
        }
    }
}
