using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface ISettingService
    {
        public Task<PagginatedResponse<SettingGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(SettingPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, SettingPostDto dto);
        public Task<SettingGetDto> GetAsync(int id);
    }
}
