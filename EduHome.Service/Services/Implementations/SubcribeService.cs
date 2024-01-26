using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Service.Extensions;
using EduHome.Service.Services.Interfaces;
using Karma.Service.Exceptions;
using Karma.Service.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Implementations
{
    public class SubcribeService : ISubcribeService
    {
        readonly IWebHostEnvironment _env;
        readonly ISubcribeRepository _SubcribeRepository;

        public SubcribeService(IWebHostEnvironment env, ISubcribeRepository SubcribeRepository)
        {
            _env = env;
            _SubcribeRepository = SubcribeRepository;
        }

        public async Task<CommonResponse> CreateAsync(SubcribePostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;


            Subcribe Subcribe = new Subcribe
            {
                email = dto.Email,
            };

            await _SubcribeRepository.AddAsync(Subcribe);
            await _SubcribeRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<SubcribeGetDto>> GetAllAsync(int page=1)
        {
            PagginatedResponse<SubcribeGetDto> pagginatedResponse = new PagginatedResponse<SubcribeGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _SubcribeRepository.GetQuery(x => !x.IsDeleted);
            

            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                  .Select(x =>
              new SubcribeGetDto
              {
                  Email= x.email
              })
                .ToListAsync();
            return pagginatedResponse;
        }

    }
}
