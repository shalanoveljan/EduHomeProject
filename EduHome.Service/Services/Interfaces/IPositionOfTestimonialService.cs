using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface IPositionOfTestimonialService
    {
        public Task<PagginatedResponse<PositionOfTestimonialGetDto>> GetAllAsync(int page = 1);

        public Task CreateAsync(PositionOfTestimonialPostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, PositionOfTestimonialPostDto dto);
        public Task<PositionOfTestimonialGetDto> GetAsync(int id);
    }
}
