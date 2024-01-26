using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface IPositionOfSpeakerService
    {
        public Task<PagginatedResponse<PositionGetDto>> GetAllAsync(int page = 1);

        public Task CreateAsync(PositionPostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, PositionPostDto dto);
        public Task<PositionGetDto> GetAsync(int id);
    }
}
