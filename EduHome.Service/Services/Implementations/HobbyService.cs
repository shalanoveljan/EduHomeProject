using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Data.Repositories.Implementations;
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
    public class HobbyService : IHobbyService
    {

        readonly IHobbyRepository _hobbyRepository;
        readonly ITeacherRepository _teacherRepository;

        public HobbyService(IHobbyRepository hobbyRepository, ITeacherRepository teacherRepository)
        {
            _hobbyRepository = hobbyRepository;
            _teacherRepository = teacherRepository;
        }

        private async Task<bool> CheckTeacher(int id)
        {
            return await _teacherRepository.GetQuery(x => !x.IsDeleted && x.Id == id).CountAsync() > 0;
        }

        public async Task<CommonResponse> CreateAsync(HobbyPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };
         
            Hobby Hobby =new Hobby();
            Hobby.Name = dto.Name;
            await _hobbyRepository.AddAsync(Hobby);
            await _hobbyRepository.SaveChangesAsync();

            return commonResponse;
        }


        public async Task<PagginatedResponse<HobbyGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<HobbyGetDto> pagginatedResponse = new PagginatedResponse<HobbyGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _hobbyRepository.GetQuery(x => !x.IsDeleted)
                .Include(x => x.HobbyTeachers)
                .ThenInclude(x=>x.Teacher)
                .AsNoTrackingWithIdentityResolution();
                
                
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                 .Take(3)
                 .Select(x => new HobbyGetDto { Name = x.Name, Id = x.Id })
                 .ToListAsync();


            return pagginatedResponse;

        }

        public async Task<HobbyGetDto> GetAsync(int id)
        {

            Hobby? Hobby = await _hobbyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Hobby == null)
            {

                throw new ItemNotFoundException("Hobby Not Found");
            }

            HobbyGetDto HobbyGetDto = new HobbyGetDto
            {
                Name = Hobby.Name,
                Id = Hobby.Id
            };

            return HobbyGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Hobby? Hobby = await _hobbyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Hobby == null)
            {
                throw new ItemNotFoundException("Hobby Not Found");
            }

            Hobby.IsDeleted = true;
            await _hobbyRepository.UpdateAsync(Hobby);
            await _hobbyRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, HobbyPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };
            Hobby? Hobby = await _hobbyRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Hobby == null)
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "Hobby Not Found";
                return commonResponse;
            }

            Hobby.Name = dto.Name;
            await _hobbyRepository.UpdateAsync(Hobby);
            await _hobbyRepository.SaveChangesAsync();

            return commonResponse;
        }
    }
}
