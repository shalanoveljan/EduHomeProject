using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface IEventService
    {
        public Task<PagginatedResponse<EventGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(EventPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, EventPostDto dto);
        public Task<EventGetDto> GetAsync(int id);
    }
}
