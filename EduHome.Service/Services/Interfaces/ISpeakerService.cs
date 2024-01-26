using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface ISpeakerService
    {
        public Task<PagginatedResponse<SpeakerGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(SpeakerPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, SpeakerPostDto dto);
        public Task<SpeakerGetDto> GetAsync(int id);
    }
}
