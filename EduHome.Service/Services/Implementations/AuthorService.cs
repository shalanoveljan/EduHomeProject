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
    public class AuthorService : IAuthorService
    {
        readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task CreateAsync(AuthorPostDto dto)
        {
            Author Author = new Author();
            Author.Name = dto.Name;

            await _authorRepository.AddAsync(Author);
            await _authorRepository.SaveChangesAsync();
        }

        public async Task<PagginatedResponse<AuthorGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<AuthorGetDto> pagginatedResponse = new PagginatedResponse<AuthorGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _authorRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTracking();
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
               .Take(3)
               .Select(x =>
               new AuthorGetDto
               {
                   Name = x.Name,
                   Id = x.Id,
               })
               .ToListAsync();

            return pagginatedResponse;
        }

        public async Task<AuthorGetDto> GetAsync(int id)
        {
            Author? Author = await _authorRepository.GetAsync(x => !x.IsDeleted && x.Id == id);


            if (Author == null)
            {
                throw new ItemNotFoundException("Author Not Found");
            }

            AuthorGetDto AuthorGetDto = new AuthorGetDto
            {
                Id = Author.Id,
                Name = Author.Name,
               
            };
            return AuthorGetDto;
        }
        public async Task RemoveAsync(int id)
        {
            Author? Author = await _authorRepository.GetAsync(x => !x.IsDeleted && x.Id == id);


            if (Author == null)
            {
                throw new ItemNotFoundException("Author Not Found");
            }
            Author.IsDeleted = true;
            await _authorRepository.UpdateAsync(Author);
            await _authorRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AuthorPostDto dto)
        {
            Author? Author = await _authorRepository.GetAsync(x => !x.IsDeleted && x.Id == id);


            if (Author == null)
            {
                throw new ItemNotFoundException("Author Not Found");
            }

            Author.Name = dto.Name;


            await _authorRepository.UpdateAsync(Author);
            await _authorRepository.SaveChangesAsync();
        }
    }
}
