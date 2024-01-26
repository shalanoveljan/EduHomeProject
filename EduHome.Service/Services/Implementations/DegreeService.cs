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
    public class DegreeService : IDegreeService
    {
        readonly IDegreeRepository _degreeRepository;

        public DegreeService(IDegreeRepository DegreeRepository)
        {
            _degreeRepository = DegreeRepository;
        }

        public async Task<CommonResponse> CreateAsync(DegreePostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200,
            };
            Degree Degree = new Degree();
            Degree.Name = dto.Name;
            await _degreeRepository.AddAsync(Degree);
            await _degreeRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<DegreeGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<DegreeGetDto> pagginatedResponse = new PagginatedResponse<DegreeGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _degreeRepository.GetQuery(x => !x.IsDeleted)
                .Include(x => x.Teachers)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new DegreeGetDto { Name = x.Name, Id = x.Id })
                .ToListAsync();

            return pagginatedResponse;
        }

        public async Task<DegreeGetDto> GetAsync(int id)
        {
            Degree? Degree = await _degreeRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Degree == null)
            {
                throw new ItemNotFoundException("Degree Not Found");
            }

            DegreeGetDto DegreeGetDto = new DegreeGetDto
            {
                Name = Degree.Name,
                Id=Degree.Id
            };
            return DegreeGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Degree? Degree = await _degreeRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Degree == null)
            {
                throw new ItemNotFoundException("Degree Not Found");
            }

            Degree.IsDeleted = true;
            await _degreeRepository.UpdateAsync(Degree);
            await _degreeRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, DegreePostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200,
            };
            Degree? Degree = await _degreeRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Degree == null)
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "Degree Not Found";
                return commonResponse;
            }

            Degree.Name = dto.Name;
            await _degreeRepository.UpdateAsync(Degree);
            await _degreeRepository.SaveChangesAsync();
            return commonResponse;
        }
    }
}
