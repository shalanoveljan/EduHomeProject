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
using System.Data.Entity;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EduHome.Service.Services.Implementations
{
    public class SettingService : ISettingService
    {
        readonly IWebHostEnvironment _env;
        readonly ISettingRepository _SettingRepository;

        public SettingService(ISettingRepository SettingRepository, IWebHostEnvironment env)
        {
            _SettingRepository = SettingRepository;
            _env = env;
        }



        public async Task<CommonResponse> CreateAsync(SettingPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200,
            };
            Setting Setting = new Setting();
            Setting.Address = dto.Address;
            Setting.VimeoUrl = dto.VimeoUrl;
            Setting.FacebookUrl = dto.FacebookUrl;
            Setting.TwitterUrl = dto.TwitterUrl;
            Setting.PinterestUrl = dto.PinterestUrl;
            Setting.PhoneNumber = dto.PhoneNumber;
            Setting.Email = dto.Email;
            Setting.WelcomeTitle= dto.WelcomeTitle;
            Setting.WelcomeDesc= dto.WelcomeDesc;
            Setting.Video= dto.Video;

            if (dto.Logo == null)
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The field image is required";
                return commonResponse;
            }

            if (!dto.Logo.IsImage())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image is not valid";
                return commonResponse;
            }

            if (dto.Logo.IsSizeOk(2))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image  size is not valid";
                return commonResponse;
            }

            Setting.Logo = dto.Logo.SaveFile(_env.WebRootPath, "assets/img/logo");

            if (dto.WelcomeImage == null)
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The field image is required";
                return commonResponse;
            }

            if (!dto.WelcomeImage.IsImage())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image is not valid";
                return commonResponse;
            }

            if (dto.WelcomeImage.IsSizeOk(2))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image  size is not valid";
                return commonResponse;
            }

            Setting.WelcomeImage = dto.WelcomeImage.SaveFile(_env.WebRootPath, "assets/img/setting");

            await _SettingRepository.AddAsync(Setting);
            await _SettingRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<SettingGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<SettingGetDto> pagginatedResponse = new PagginatedResponse<SettingGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _SettingRepository.GetQuery(x => !x.IsDeleted);
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items =  query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new SettingGetDto
                 {
                     FacebookUrl = x.FacebookUrl,
                     PinterestUrl = x.PinterestUrl,
                     VimeoUrl = x.VimeoUrl,
                     TwitterUrl = x.TwitterUrl,
                     Address = x.Address,
                     Email = x.Email,
                     PhoneNumber = x.PhoneNumber,
                     Logo = x.Logo,
                     Video= x.Video,
                     Id = x.Id,
                     WelcomeDesc= x.WelcomeDesc,
                     WelcomeImage= x.WelcomeImage,
                     WelcomeTitle= x.WelcomeTitle,

                 })
                .ToList();

            return pagginatedResponse;
        }

        public async Task<SettingGetDto> GetAsync(int id)
        {
            Setting? Setting = await _SettingRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Setting == null)
            {
                throw new ItemNotFoundException("Setting Not Found");
            }

            SettingGetDto SettingGetDto = new SettingGetDto
            {
                FacebookUrl = Setting.FacebookUrl,
                PinterestUrl = Setting.PinterestUrl,
                VimeoUrl = Setting.VimeoUrl,
                TwitterUrl = Setting.TwitterUrl,
                Address = Setting.Address,
                Email = Setting.Email,
                PhoneNumber = Setting.PhoneNumber,
                Logo = Setting.Logo,
                Id = Setting.Id,
                WelcomeImage= Setting.WelcomeImage,
                WelcomeDesc= Setting.WelcomeDesc,
                WelcomeTitle= Setting.WelcomeTitle,
                Video= Setting.Video,
            };
            return SettingGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Setting? Setting = await _SettingRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Setting == null)
            {
                throw new ItemNotFoundException("Setting Not Found");
            }

            Setting.IsDeleted = true;
            await _SettingRepository.UpdateAsync(Setting);
            await _SettingRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, SettingPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200,
            };
            Setting? Setting = await _SettingRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Setting == null)
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "Setting Not Found";
                return commonResponse;
            }

            Setting.FacebookUrl = dto.FacebookUrl;
            Setting.Address = dto.Address;
            Setting.Email = dto.Email;
            Setting.PhoneNumber = dto.PhoneNumber;
            Setting.FacebookUrl = dto.FacebookUrl;
            Setting.TwitterUrl = dto.TwitterUrl;
            Setting.VimeoUrl = dto.VimeoUrl;
            Setting.PinterestUrl = dto.PinterestUrl;
            Setting.Video = dto.Video;
            if (dto.Logo != null)
            {
                if (!dto.Logo.IsImage())
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image is not valid";
                    return commonResponse;
                }

                if (dto.Logo.IsSizeOk(2))
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image  size is not valid";
                    return commonResponse;
                }

                Setting.Logo = dto.Logo.SaveFile(_env.WebRootPath, "assets/img/logo");
            }

          

            if (dto.WelcomeImage != null)
            {
                if (!dto.WelcomeImage.IsImage())
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image is not valid";
                    return commonResponse;
                }

                if (dto.WelcomeImage.IsSizeOk(2))
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image  size is not valid";
                    return commonResponse;
                }

                Setting.WelcomeImage = dto.WelcomeImage.SaveFile(_env.WebRootPath, "assets/img/setting");
            }

         

            await _SettingRepository.UpdateAsync(Setting);
            await _SettingRepository.SaveChangesAsync();
            return commonResponse;
        }
    }
}
