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
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Implementations
{
    public class TestimonialService : ITestimonialService
    {
        readonly IWebHostEnvironment _env;
        readonly ITestimonialRepository _testimonialRepository;

        public TestimonialService(IWebHostEnvironment env, ITestimonialRepository TestimonialRepository)
        {
            _env = env;
            _testimonialRepository = TestimonialRepository;
        }

        public async Task<CommonResponse> CreateAsync(TestimonialPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;


            Testimonial Testimonial = new Testimonial
            {
                Description = dto.Description,
                Name = dto.Name,
                TestimonialPositions = new List<TestimonialPosition>()
        };

            if (dto.Image == null)
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The field image is required";
                return commonResponse;
            }

            if (!dto.Image.IsImage())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image is not valid";
                return commonResponse;
            }

            if (dto.Image.IsSizeOk(2))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image  size is not valid";
                return commonResponse;
            }


            Testimonial.Image = dto.Image.SaveFile(_env.WebRootPath, "assets/img/testimonial");

            foreach (var item in dto.PositionsIds)
            {
                Testimonial.TestimonialPositions.Add(new TestimonialPosition
                {
                    Testimonial = Testimonial,
                    PositionOfTestimonialId = item,
                });
            }


            await _testimonialRepository.AddAsync(Testimonial);
            await _testimonialRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<TestimonialGetDto>> GetAllAsync(int page=1)
        {
            PagginatedResponse<TestimonialGetDto> pagginatedResponse = new PagginatedResponse<TestimonialGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _testimonialRepository.GetQuery(x => !x.IsDeleted)
                .AsNoTrackingWithIdentityResolution()
                .Include(x=>x.TestimonialPositions)
                .ThenInclude(x=>x.PositionOfTestimonial);
            

            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                  .Select(x =>
              new TestimonialGetDto
              {
                  Name = x.Name,
                  Id = x.Id,
                  Description = x.Description,
                  Image = x.Image,
                  PositionOfTestimonials = x.TestimonialPositions.Select(y => new PositionOfTestimonialGetDto { Name = y.PositionOfTestimonial.Name, Id = y.PositionOfTestimonial.Id }),

              })
                .ToListAsync();
            return pagginatedResponse;
        }

        public async Task<TestimonialGetDto> GetAsync(int id)
        {
            Testimonial? Testimonial = await _testimonialRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Testimonial == null)
            {
                throw new ItemNotFoundException("Testimonial Not Found");
            }

            TestimonialGetDto TestimonialGetDto = new TestimonialGetDto
            {
                Id = Testimonial.Id,
                Description = Testimonial.Description,
                Image = Testimonial.Image,
                Name = Testimonial.Name,
                PositionOfTestimonials = Testimonial.TestimonialPositions.Select(y => new PositionOfTestimonialGetDto { Name = y.PositionOfTestimonial.Name, Id = y.PositionOfTestimonial.Id }),

            };

            return TestimonialGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Testimonial? Testimonial = await _testimonialRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Testimonial == null)
            {
                throw new ItemNotFoundException("Testimonial Not Found");
            }
            Testimonial.IsDeleted = true;
            await _testimonialRepository.UpdateAsync(Testimonial);
            await _testimonialRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, TestimonialPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;
            Testimonial? Testimonial = await _testimonialRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Testimonial == null)
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "Testimonial Not Found";
                return commonResponse;

            }
            Testimonial.Name = dto.Name;
            Testimonial.Description = dto.Description;


            if (dto.Image != null)
            {
                if (!dto.Image.IsImage())
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image is not valid";
                    return commonResponse;
                }

                if (dto.Image.IsSizeOk(2))
                {       
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image  size is not valid";
                    return commonResponse;
                }

                Testimonial.Image = dto.Image.SaveFile(_env.WebRootPath, "assets/img/testimonial");

            }
            Testimonial.TestimonialPositions.Clear();
            foreach (var item in dto.PositionsIds)
            {
                Testimonial.TestimonialPositions.Add(new TestimonialPosition
                {
                    Testimonial = Testimonial,
                    PositionOfTestimonialId = item,
                });
            }

            await _testimonialRepository.UpdateAsync(Testimonial);
            await _testimonialRepository.SaveChangesAsync();
            return commonResponse;
        }
    }
}
