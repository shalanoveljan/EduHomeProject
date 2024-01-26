using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Service.Services.Interfaces;
using Karma.Service.Exceptions;
using Karma.Service.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Implementations
{
    public class FacultyService : IFacultyService
    {
        readonly IFacultyRepository _facultyRepository;

        public FacultyService(IFacultyRepository TagRepository)
        {
            _facultyRepository = TagRepository;
        }

        public async Task<CommonResponse> CreateAsync(FacultyPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200,
            };
            Faculty faculty = new Faculty();
            faculty.Name = dto.Name;
            await _facultyRepository.AddAsync(faculty);
            await _facultyRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<FacultyGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<FacultyGetDto> pagginatedResponse = new PagginatedResponse<FacultyGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _facultyRepository.GetQuery(x => !x.IsDeleted)
                .Include(x => x.FacultyTeachers)
                .ThenInclude(x => x.Teacher)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new FacultyGetDto { Name = x.Name, Id = x.Id })
                 .ToListAsync();


            return pagginatedResponse;
        }

        public async Task<FacultyGetDto> GetAsync(int id)
        {
            Faculty? Faculty = await _facultyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Faculty == null)
            {
                throw new ItemNotFoundException("Faculty Not Found");
            }

            FacultyGetDto TagGetDto = new FacultyGetDto
            {
                Id = Faculty.Id,
                Name = Faculty.Name
            };
            return TagGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Faculty? Faculty = await _facultyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Faculty == null)
            {
                throw new ItemNotFoundException("Faculty Not Found");
            }
            Faculty.IsDeleted = true;
            await _facultyRepository.UpdateAsync(Faculty);
            await _facultyRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, FacultyPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200,
            };
            Faculty? Faculty = await _facultyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Faculty == null)
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "Faculty Not Found";
                return commonResponse;
            }

            Faculty.Name = dto.Name;
            await _facultyRepository.UpdateAsync(Faculty);
            await _facultyRepository.SaveChangesAsync();

            return commonResponse;
        }
    }
}
