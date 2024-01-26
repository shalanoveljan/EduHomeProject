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
    public class PositionOfSpeakerService : IPositionOfSpeakerService
    {
        readonly IPositionOfSpeakerRepository _positionRepository;

        public PositionOfSpeakerService(IPositionOfSpeakerRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task CreateAsync(PositionPostDto dto)
        {
            PositionOfSpeaker position = new PositionOfSpeaker();
            position.Name = dto.Name;
            await _positionRepository.AddAsync(position);
            await _positionRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<PositionGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<PositionGetDto> pagginatedResponse = new PagginatedResponse<PositionGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _positionRepository.GetQuery(x => !x.IsDeleted)
                .Include(x => x.Speakers)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new PositionGetDto { Name = x.Name, Id = x.Id })
                .ToListAsync();

            return pagginatedResponse;
        }

        public async Task<PositionGetDto> GetAsync(int id)
        {
            PositionOfSpeaker? Position = await _positionRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Position == null)
            {
                throw new ItemNotFoundException("Position Not Found");
            }

            PositionGetDto PositionGetDto = new PositionGetDto
            {
                Name = Position.Name,
                Id = Position.Id,
                 
            };
            return PositionGetDto;
        }

        public  async Task RemoveAsync(int id)
        {
            PositionOfSpeaker? Position = await _positionRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Position == null)
            {
                throw new ItemNotFoundException("Position Not Found");
            }

            Position.IsDeleted = true;
            await _positionRepository.UpdateAsync(Position);
            await _positionRepository.SaveChangesAsync();
        }

        public async  Task UpdateAsync(int id, PositionPostDto dto)
        {
            PositionOfSpeaker? Position = await _positionRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

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
