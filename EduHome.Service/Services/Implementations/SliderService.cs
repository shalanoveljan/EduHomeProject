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
    public class SliderService : ISliderService
    {
        readonly IWebHostEnvironment _env;
        readonly ISliderRepository _SliderRepository;

        public SliderService(IWebHostEnvironment env, ISliderRepository SliderRepository)
        {
            _env = env;
            _SliderRepository = SliderRepository;
        }

        public async Task<CommonResponse> CreateAsync(SliderPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;


            Slider Slider = new Slider
            {
                Description = dto.Description,
                Title = dto.Title,
                Url= dto.Url,
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


            Slider.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/slider");


            await _SliderRepository.AddAsync(Slider);
            await _SliderRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<SliderGetDto>> GetAllAsync(int page=1)
        {
            PagginatedResponse<SliderGetDto> pagginatedResponse = new PagginatedResponse<SliderGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _SliderRepository.GetQuery(x => !x.IsDeleted);
            

            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                  .Select(x =>
              new SliderGetDto
              {
                  Title = x.Title,
                  Id = x.Id,
                  Description = x.Description,
                  Image = x.Image,
                  Url=x.Url,
              })
                .ToListAsync();
            return pagginatedResponse;
        }

        public async Task<SliderGetDto> GetAsync(int id)
        {
            Slider? Slider = await _SliderRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Slider == null)
            {
                throw new ItemNotFoundException("Slider Not Found");
            }

            SliderGetDto SliderGetDto = new SliderGetDto
            {
                Id = Slider.Id,
                Description = Slider.Description,
                Image = Slider.Image,
                Title = Slider.Title,
                Url= Slider.Url
            };
            return SliderGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Slider? Slider = await _SliderRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Slider == null)
            {
                throw new ItemNotFoundException("Slider Not Found");
            }
            Slider.IsDeleted = true;
            await _SliderRepository.UpdateAsync(Slider);
            await _SliderRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, SliderPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;
            Slider? Slider = await _SliderRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Slider == null)
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "Slider Not Found";
                return commonResponse;

            }
            Slider.Title = dto.Title;
            Slider.Description = dto.Description;
            Slider.Url= dto.Url;


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

                Slider.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/slider");
            }

            await _SliderRepository.UpdateAsync(Slider);
            await _SliderRepository.SaveChangesAsync();
            return commonResponse;
        }
    }
}
