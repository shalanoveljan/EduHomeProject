using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface ITestimonialService
    {
        public Task<PagginatedResponse<TestimonialGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(TestimonialPostDto dto);

        public Task RemoveAsync(int id);

        public Task<CommonResponse> UpdateAsync(int id, TestimonialPostDto dto);
        public Task<TestimonialGetDto> GetAsync(int id);
    }
}
