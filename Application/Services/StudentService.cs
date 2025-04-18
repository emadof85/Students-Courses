﻿using Application.DTOs;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IRepository<Course> _courseRepo;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository studentRepository, IRepository<Course> courseRepo, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
            //return students.Select(s => new StudentDto { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName });
        }

        public async Task<StudentDto> GetByIdAsync(string id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task CreateAsync(CreateStudentDto studentDto)
        {
            /*var student = new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                PhoneNumber = studentDto.PhoneNumber,
                Age = studentDto.Age,
                StudentNumber = studentDto.StudentNumber
            };*/
            var student = _mapper.Map<Student>(studentDto);
            student.UserName = studentDto.Email;
            student.Email = studentDto.Email;
            student.EmailConfirmed = true;
            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateStudentDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(dto.Id);
            if (student == null) throw new Exception("Student not found");

            _mapper.Map(dto, student);
            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) throw new Exception("Student not found");

            _studentRepository.Delete(student);
            await _studentRepository.SaveChangesAsync();
        }

        public async Task EnrollInCourse(string studentId, int courseId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            var course = await _courseRepo.GetByIdAsync(courseId);
            if (student == null || course == null) throw new Exception("Student or course not found");

            student.StudentCourses ??= new List<StudentCourse>();
            student.StudentCourses.Add(new StudentCourse { CourseId = courseId, StudentId = studentId });

            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync();
        }
    }
}
