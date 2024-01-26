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
    public class DutyService : IDutyService
    {
        readonly IDutyRepository _dutyRepository;

        public DutyService(IDutyRepository DutyRepository)
        {
            _dutyRepository = DutyRepository;
        }

        public async Task<CommonResponse> CreateAsync(DutyPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200,
            };
            Duty Duty = new Duty();
            Duty.Name = dto.Name;
            await _dutyRepository.AddAsync(Duty);
            await _dutyRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<DutyGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<DutyGetDto> pagginatedResponse = new PagginatedResponse<DutyGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _dutyRepository.GetQuery(x => !x.IsDeleted)
                .Include(x => x.Teachers)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new DutyGetDto { Name = x.Name, Id = x.Id })
                .ToListAsync();

            return pagginatedResponse;
        }

        public async Task<DutyGetDto> GetAsync(int id)
        {
            Duty? Duty = await _dutyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Duty == null)
            {
                throw new ItemNotFoundException("Duty Not Found");
            }

            DutyGetDto DutyGetDto = new DutyGetDto
            {
                Name = Duty.Name,
                Id= Duty.Id
            };
            return DutyGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Duty? Duty = await _dutyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Duty == null)
            {
                throw new ItemNotFoundException("Duty Not Found");
            }

            Duty.IsDeleted = true;
            await _dutyRepository.UpdateAsync(Duty);
            await _dutyRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, DutyPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200,
            };
            Duty? Duty = await _dutyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Duty == null)
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "Duty Not Found";
                return commonResponse;
            }

            Duty.Name = dto.Name;
            await _dutyRepository.UpdateAsync(Duty);
            await _dutyRepository.SaveChangesAsync();
            return commonResponse;
        }
    }
}
