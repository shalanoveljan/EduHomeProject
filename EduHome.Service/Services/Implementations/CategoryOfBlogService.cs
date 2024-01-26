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
    public class CategoryOfBlogService : ICategoryOfBlogService
    {
        readonly ICategoryOfBlogRepository _categoryRepository;

        public CategoryOfBlogService(ICategoryOfBlogRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CategoryOfBlogPostDto dto)
        {
            CategoryOfBlog category = new CategoryOfBlog();
            category.Name = dto.Name;
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<CategoryOfBlogGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<CategoryOfBlogGetDto> pagginatedResponse = new PagginatedResponse<CategoryOfBlogGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _categoryRepository.GetQuery(x => !x.IsDeleted)
                .Include(x=>x.Blogs)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new CategoryOfBlogGetDto { Name=x.Name, Id = x.Id, BlogCount = x.Blogs.Where(x => !x.IsDeleted).Count() })
                .ToListAsync();

            return pagginatedResponse;
        }

        public async  Task<CategoryOfBlogGetDto> GetAsync(int id)
        {
            CategoryOfBlog? Category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Category == null)
            {
                throw new ItemNotFoundException("Category Not Found");
            }

            CategoryOfBlogGetDto categoryOfBlogGetDto = new CategoryOfBlogGetDto
            {
               Name= Category.Name,
               Id = Category.Id,
            };
            return categoryOfBlogGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            CategoryOfBlog? Category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Category == null)
            {
                throw new ItemNotFoundException("Category Not Found");
            }

            Category.IsDeleted = true;
            await _categoryRepository.UpdateAsync(Category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CategoryOfBlogPostDto dto)
        {
            CategoryOfBlog? Category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Category == null)
            {
                throw new ItemNotFoundException("Category Not Found");
            }

            Category.Name = dto.Name;
            await _categoryRepository.UpdateAsync(Category);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
