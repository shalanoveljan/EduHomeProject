using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface IDegreeService
    {
        public Task<PagginatedResponse<DegreeGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(DegreePostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, DegreePostDto dto);
        public Task<DegreeGetDto> GetAsync(int id);
    }
}
