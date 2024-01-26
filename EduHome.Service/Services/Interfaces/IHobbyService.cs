using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface IHobbyService
    {
        public Task<PagginatedResponse<HobbyGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(HobbyPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, HobbyPostDto dto);
        public Task<HobbyGetDto> GetAsync(int id);
    }
}
