using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface IDutyService
    {
        public Task<PagginatedResponse<DutyGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(DutyPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, DutyPostDto dto);
        public Task<DutyGetDto> GetAsync(int id);
    }
}
