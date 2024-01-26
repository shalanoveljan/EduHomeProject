using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface IFacultyService
    {
        public Task<PagginatedResponse<FacultyGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(FacultyPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, FacultyPostDto dto);
        public Task<FacultyGetDto> GetAsync(int id);
    }
}
