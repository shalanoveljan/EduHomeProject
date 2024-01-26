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
    public class CategoryOfEventService : ICategoryOfEventService
    {
        readonly ICategoryOfEventRepository _categoryRepository;

        public CategoryOfEventService(ICategoryOfEventRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CategoryOfEventPostDto dto)
        {
            CategoryOfEvent category = new CategoryOfEvent();
            category.Name = dto.Name;
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<CategoryOfEventGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<CategoryOfEventGetDto> pagginatedResponse = new PagginatedResponse<CategoryOfEventGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _categoryRepository.GetQuery(x => !x.IsDeleted)
                .Include(x => x.Events)
                .AsNoTrackingWithIdentityResolution();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new CategoryOfEventGetDto { Name = x.Name, Id = x.Id, EventCount = x.Events.Where(x => !x.IsDeleted).Count() })
                .ToListAsync();

            return pagginatedResponse;
        }

        public async Task<CategoryOfEventGetDto> GetAsync(int id)
        {
            CategoryOfEvent? Category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Category == null)
            {
                throw new ItemNotFoundException("Category Not Found");
            }

            CategoryOfEventGetDto categoryOfEventGetDto = new CategoryOfEventGetDto
            {
                Name = Category.Name,
                Id = Category.Id
            };
            return categoryOfEventGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            CategoryOfEvent? Category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Category == null)
            {
                throw new ItemNotFoundException("Category Not Found");
            }

            Category.IsDeleted = true;
            await _categoryRepository.UpdateAsync(Category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CategoryOfEventPostDto dto)
        {
            CategoryOfEvent? Category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

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
