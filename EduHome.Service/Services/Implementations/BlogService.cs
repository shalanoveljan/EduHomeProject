using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Service.Extensions;
using EduHome.Service.Services.Interfaces;
using Karma.Service.Exceptions;
using Karma.Service.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Implementations
{
    public class BlogService : IBlogService
    {
        readonly IWebHostEnvironment _env;
        readonly IBlogRepository _blogRepository;

        public BlogService(IWebHostEnvironment env, IBlogRepository blogRepository)
        {
            _env = env;
            _blogRepository = blogRepository;
        }

        public async Task<CommonResponse> CreateAsync(BlogPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;


            Blog blog = new Blog
            {
                AuthorId = dto.AuthorId,
                Description = dto.Description,
                Title = dto.Title,
                CategoryIdOfBlog= dto.CategoryIdOfBlog,
                TagsBlog = new List<TagBlog>()
            };

            if (dto.ImageFile == null)
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The field image is required";
                return commonResponse;
            }

            if (!dto.ImageFile.IsImage())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image is not valid";
                return commonResponse;
            }

            if (dto.ImageFile.IsSizeOk(2))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image  size is not valid";
                return commonResponse;
            }

            blog.Storage = "wwwroot";

            blog.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/blog");

            foreach (var item in dto.TagsIdsOfBlog)
            {
                blog.TagsBlog.Add(new TagBlog
                {
                    Blog = blog,
                    TagIdOfBlog = item,
                });
            }

            await _blogRepository.AddAsync(blog);
            await _blogRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<BlogGetDto>> GetAllAsync(int page=1)
        {
            PagginatedResponse<BlogGetDto> pagginatedResponse = new PagginatedResponse<BlogGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _blogRepository.GetQuery(x => !x.IsDeleted)
             .AsNoTrackingWithIdentityResolution()
             .Include(x => x.TagsBlog)
             .ThenInclude(x => x.TagOfBlog)
             .Include(x => x.Author)
             .Include(x=>x.CategoryOfBlog);

            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                  .Select(x =>
              new BlogGetDto
              {
                  Title = x.Title,
                  Id = x.Id,
                  Description = x.Description,
                  TagsOfBlog = x.TagsBlog.Select(y => new TagOfBlogGetDto { Name = y.TagOfBlog.Name, Id = y.TagIdOfBlog }),
                  Image = x.Image,
                  Date = x.CreatedAt,
                  AuthorGetDto = new AuthorGetDto { Name = x.Author.Name },
                  Category=new CategoryOfBlogGetDto { Name=x.CategoryOfBlog.Name },
              })
                .ToListAsync();
            return pagginatedResponse;
        }





        public async Task<PagginatedResponse<BlogGetDto>> GetBlogsBySearchTextAsync(string searchText,int page)
        {

            if (string.IsNullOrEmpty(searchText))
            {
               return await  GetAllAsync(page);
            }
            PagginatedResponse<BlogGetDto> pagginatedResponse = new PagginatedResponse<BlogGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _blogRepository.GetQuery(x => !x.IsDeleted)
                      .Where(m=>m.Title.ToLower().Contains(searchText.ToLower()))

             .AsNoTrackingWithIdentityResolution()
             .Include(x => x.TagsBlog)
             .ThenInclude(x => x.TagOfBlog)
             .Include(x => x.Author)
             .Include(x => x.CategoryOfBlog);

            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);


            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                    .Take(3)
                      .Select(x =>
                  new BlogGetDto
                  {
                      Title = x.Title,
                      Id = x.Id,
                      Description = x.Description,
                      TagsOfBlog = x.TagsBlog.Select(y => new TagOfBlogGetDto { Name = y.TagOfBlog.Name, Id = y.TagIdOfBlog }),
                      Image = x.Image,
                      Date = x.CreatedAt,
                      AuthorGetDto = new AuthorGetDto { Name = x.Author.Name },
                      Category = new CategoryOfBlogGetDto { Name = x.CategoryOfBlog.Name },
                  })
                    .ToListAsync();
            return pagginatedResponse;



            //return await _blogRepository.GetQuery(x => !x.IsDeleted).AsNoTrackingWithIdentityResolution()
            //    .Include(x => x.Author)
            //     .Select(x => new BlogGetDto
            //     {
            //         Id = x.Id,
            //         Title = x.Title,
            //         Description = x.Description,
            //         Image = x.Image,
            //         AuthorGetDto = new AuthorGetDto { Name = x.Author.Name},
            //         Date = x.CreatedAt,


            //     }).Where(m => m.Title.ToLower().Contains(searchText.ToLower())).ToListAsync();
        }





        public async Task<BlogGetDto> GetAsync(int id)
        {
            Blog? blog = await _blogRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagsBlog.TagOfBlog", "Author", "CategoryOfBlog","Comments");

            if (blog == null)
            {
                throw new ItemNotFoundException("Blog Not Found");
            }

            BlogGetDto? BlogGetDto = new BlogGetDto
            {
                Id = blog.Id,
                Date = blog.CreatedAt,
                Description = blog.Description,
                Image = blog.Image,
                TagsOfBlog = blog.TagsBlog.Select(x => new TagOfBlogGetDto { Name = x.TagOfBlog.Name, Id = x.TagOfBlog.Id }),
                Title = blog.Title,
                AuthorGetDto = new AuthorGetDto
                {
                    Name = blog.Author.Name,
                    Id = blog.Author.Id,
                },
                Category = new CategoryOfBlogGetDto { Name = blog.CategoryOfBlog.Name,Id=blog.CategoryOfBlog.Id, BlogCount = blog.CategoryOfBlog.Blogs.Where(x => !x.IsDeleted).Count() },
                Comments = blog.Comments,

            };
            return BlogGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Blog? blog = await _blogRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagsBlog.TagOfBlog", "Author", "CategoryOfBlog");

            if (blog == null)
            {
                throw new ItemNotFoundException("Blog Not Found");
            }
            blog.IsDeleted = true;
            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, BlogPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;
            Blog? blog = await _blogRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagsBlog.TagOfBlog", "Author", "CategoryOfBlog");

            if (blog == null)
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "Blog Not Found";
                return commonResponse;
            }
            blog.Title = dto.Title;
            blog.Description = dto.Description;
            blog.AuthorId = dto.AuthorId;
            blog.CategoryIdOfBlog= dto.CategoryIdOfBlog;

            if (dto.ImageFile != null)
            {
                if (!dto.ImageFile.IsImage())
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image is not valid";
                    return commonResponse;
                }

                if (dto.ImageFile.IsSizeOk(2))
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image  size is not valid";
                    return commonResponse;
                }

                blog.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/blog");
            }


            blog.TagsBlog.Clear();

            foreach (var item in dto.TagsIdsOfBlog)
            {
                blog.TagsBlog.Add(new TagBlog
                {
                    Blog = blog,
                    TagIdOfBlog = item,
                });
            }

            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveChangesAsync();
            return commonResponse;
        }
    }
}
