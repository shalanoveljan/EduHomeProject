using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface IBoardService
    {
        public Task<PagginatedResponse<BoardGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(BoardPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, BoardPostDto dto);
        public Task<BoardGetDto> GetAsync(int id);
    }
}
