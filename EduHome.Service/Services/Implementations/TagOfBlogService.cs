using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Karma.Service.Exceptions;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduHome.Service.Services.Implementations
{
    public class TagOfBlogService : ITagOfBlogService
    {
        readonly ITagOfBlogRepository _TagRepository;

        public TagOfBlogService(ITagOfBlogRepository TagRepository)
        {
            _TagRepository = TagRepository;
        }

        public async Task CreateAsync(TagOfBlogPostDto dto)
        {
            TagOfBlog Tag = new TagOfBlog();
            Tag.Name = dto.Name;
            await _TagRepository.AddAsync(Tag);
            await _TagRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<TagOfBlogGetDto>> GetAllAsync(int page=1)
        {
            PagginatedResponse<TagOfBlogGetDto> pagginatedResponse = new PagginatedResponse<TagOfBlogGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _TagRepository.GetQuery(x => !x.IsDeleted)
                .Include(x => x.TagsBlog)
                .ThenInclude(x=>x.Blog)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new TagOfBlogGetDto { Name = x.Name, Id = x.Id})
                 .ToListAsync();
                 

            return pagginatedResponse;
        }

        public async Task<TagOfBlogGetDto> GetAsync(int id)
        {
            TagOfBlog? Tag = await _TagRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Tag == null)
            {
                throw new ItemNotFoundException("Tag Not Found");
            }

            TagOfBlogGetDto TagGetDto = new TagOfBlogGetDto
            {
                Id = Tag.Id,
                Name = Tag.Name
            };
            return TagGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            TagOfBlog? Tag = await _TagRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Tag == null)
            {
                throw new ItemNotFoundException("Tag Not Found");
            }
            Tag.IsDeleted = true;
            await _TagRepository.UpdateAsync(Tag);
            await _TagRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TagOfBlogPostDto dto)
        {
            TagOfBlog? Tag = await _TagRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

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
