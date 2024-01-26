using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Service.Services.Interfaces;
using Karma.Service.Exceptions;
using Karma.Service.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Implementations
{
    public class PositionOfTestimonialService : IPositionOfTestimonialService
    {
        readonly IPositionOfTestimonialRepository _positionRepository;

        public PositionOfTestimonialService(IPositionOfTestimonialRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task CreateAsync(PositionOfTestimonialPostDto dto)
        {
            PositionOfTestimonial position = new PositionOfTestimonial();
            position.Name = dto.Name;
            await _positionRepository.AddAsync(position);
            await _positionRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<PositionOfTestimonialGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<PositionOfTestimonialGetDto> pagginatedResponse = new PagginatedResponse<PositionOfTestimonialGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _positionRepository.GetQuery(x => !x.IsDeleted)
                .Include(x => x.TestimonialPositions)
                .ThenInclude(x=>x.Testimonial)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new PositionOfTestimonialGetDto { Name = x.Name, Id = x.Id })
                .ToListAsync();

            return pagginatedResponse;
        }

        public async Task<PositionOfTestimonialGetDto> GetAsync(int id)
        {
            PositionOfTestimonial? Position = await _positionRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Position == null)
            {
                throw new ItemNotFoundException("Position Not Found");
            }

            PositionOfTestimonialGetDto PositionOfTestimonialGetDto = new PositionOfTestimonialGetDto
            {
                Name = Position.Name,
                Id = Position.Id,
                 
            };
            return PositionOfTestimonialGetDto;
        }

        public  async Task RemoveAsync(int id)
        {
            PositionOfTestimonial? Position = await _positionRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Position == null)
            {
                throw new ItemNotFoundException("Position Not Found");
            }

            Position.IsDeleted = true;
            await _positionRepository.UpdateAsync(Position);
            await _positionRepository.SaveChangesAsync();
        }

        public async  Task UpdateAsync(int id, PositionOfTestimonialPostDto dto)
        {
            PositionOfTestimonial? Position = await _positionRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Position == null)
            {
                throw new ItemNotFoundException("Position Not Found");
            }
            Position.Name= dto.Name;
            await _positionRepository.UpdateAsync(Position);
            await _positionRepository.SaveChangesAsync();

        }
    }
}
