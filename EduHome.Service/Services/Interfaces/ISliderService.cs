using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface ISliderService
    {
        public Task<PagginatedResponse<SliderGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(SliderPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, SliderPostDto dto);
        public Task<SliderGetDto> GetAsync(int id);
    }
}
