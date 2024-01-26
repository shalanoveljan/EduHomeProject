using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface ISubcribeService
    {
        public Task<PagginatedResponse<SubcribeGetDto>> GetAllAsync(int page = 1);
        public Task<CommonResponse> CreateAsync(SubcribePostDto dto);
    }
}
