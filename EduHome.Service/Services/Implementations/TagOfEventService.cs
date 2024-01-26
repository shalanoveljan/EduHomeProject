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
    public class TagOfEventService : ITagOfEventService
    {
        readonly ITagOfEventRepository _TagRepository;

        public TagOfEventService(ITagOfEventRepository TagRepository)
        {
            _TagRepository = TagRepository;
        }

        public async Task CreateAsync(TagOfEventPostDto dto)
        {
            TagOfEvent Tag = new TagOfEvent();
            Tag.Name = dto.Name;
            await _TagRepository.AddAsync(Tag);
            await _TagRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<TagOfEventGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<TagOfEventGetDto> pagginatedResponse = new PagginatedResponse<TagOfEventGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _TagRepository.GetQuery(x => !x.IsDeleted)
                .Include(x => x.TagsEvent)
                .ThenInclude(x => x.Event)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new TagOfEventGetDto { Name = x.Name, Id = x.Id })
                 .ToListAsync();


            return pagginatedResponse;
        }

        public async Task<TagOfEventGetDto> GetAsync(int id)
        {
            TagOfEvent? Tag = await _TagRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Tag == null)
            {
                throw new ItemNotFoundException("Tag Not Found");
            }

            TagOfEventGetDto TagGetDto = new TagOfEventGetDto
            {
                Id = Tag.Id,
                Name = Tag.Name
            };
            return TagGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            TagOfEvent? Tag = await _TagRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Tag == null)
            {
                throw new ItemNotFoundException("Tag Not Found");
            }
            Tag.IsDeleted = true;
            await _TagRepository.UpdateAsync(Tag);
            await _TagRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TagOfEventPostDto dto)
        {
            TagOfEvent? Tag = await _TagRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Tag == null)
            {
                throw new ItemNotFoundException("Tag Not Found");
            }

            Tag.Name = dto.Name;
            await _TagRepository.UpdateAsync(Tag);
            await _TagRepository.SaveChangesAsync();
        }
    }
}
