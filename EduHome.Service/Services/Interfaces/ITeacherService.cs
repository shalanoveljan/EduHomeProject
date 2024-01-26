using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface ITeacherService
    {
        public Task<PagginatedResponse<TeacherGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(TeacherPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, TeacherPostDto dto);
        public Task<TeacherGetDto> GetAsync(int id);
    }
}
