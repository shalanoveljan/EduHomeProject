using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Service.Extensions;
using EduHome.Service.Services.Interfaces;
using Karma.Service.Exceptions;
using Karma.Service.Helpers;
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
    public class SpeakerService : ISpeakerService
    {
        readonly ISpeakerRepository _speakerRepository;
        readonly IPositionOfSpeakerRepository _positionOfSpeakerRepository;
        readonly IWebHostEnvironment _env;


        public SpeakerService(ISpeakerRepository speakerRepository, IPositionOfSpeakerRepository positionOfSpeakerRepository, IWebHostEnvironment env)
        {
            _speakerRepository = speakerRepository;
            _positionOfSpeakerRepository = positionOfSpeakerRepository;
            _env = env;
        }

        public async Task<CommonResponse> CreateAsync(SpeakerPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };

            if (dto.ImageFile == null)
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The field image is required";
                return commonResponse;
            }


            if (!await CheckPosition(dto.PositionId))
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "The Position is not valid";
                return commonResponse;
            }

            Speaker Speaker = new Speaker();
            Speaker.FullName = dto.FullName;
            Speaker.PositionId = dto.PositionId;
            Speaker.Storage = "wwwroot";
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

            Speaker.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/event");
            await _speakerRepository.AddAsync(Speaker);
            await _speakerRepository.SaveChangesAsync();
            return commonResponse;
        }

      

        public async Task<PagginatedResponse<SpeakerGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<SpeakerGetDto> pagginatedResponse = new PagginatedResponse<SpeakerGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _speakerRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution()
               .Include(x => x.Position);
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
               .Take(3)
               .Select(x =>
               new SpeakerGetDto
               {
                   FullName = x.FullName,
                   Id = x.Id,
                   Position = new PositionGetDto { Name = x.Position.Name },
                   Image = x.Image
               })
               .ToListAsync();

            return pagginatedResponse;
        }

        public async Task<SpeakerGetDto> GetAsync(int id)
        {
            Speaker? Speaker = await _speakerRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "Position");


            if (Speaker == null)
            {
                throw new ItemNotFoundException("Speaker Not Found");
            }

            SpeakerGetDto SpeakerGetDto = new SpeakerGetDto
            {
                Id = Speaker.Id,
                FullName = Speaker.FullName,
                Position = new PositionGetDto { Name = Speaker.Position.Name },
                Image = Speaker.Image
            };
            return SpeakerGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Speaker? Speaker = await _speakerRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Speaker == null)
            {
                throw new ItemNotFoundException("Speaker Not Found");
            }
            Speaker.IsDeleted = true;
            await _speakerRepository.UpdateAsync(Speaker);
            await _speakerRepository.SaveChangesAsync();
        }


        public async Task<CommonResponse> UpdateAsync(int id, SpeakerPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };
            if (!await CheckPosition(dto.PositionId))
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "The Position is not valid";
                return commonResponse;
            }

            Speaker? Speaker = await _speakerRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Speaker == null)
            {
                throw new ItemNotFoundException("Speaker Not Found");
            }

            Speaker.FullName = dto.FullName;
            Speaker.PositionId = dto.PositionId;
            
            

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
                Helper.RmoveFile(_env.WebRootPath, "img/author", Speaker.Image);
                Speaker.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/event");
            }

            await _speakerRepository.UpdateAsync(Speaker);
            await _speakerRepository.SaveChangesAsync();
            return commonResponse;
        }


        private async Task<bool> CheckPosition(int id)
        {
            return await _positionOfSpeakerRepository.GetQuery(x => !x.IsDeleted && x.Id == id).CountAsync() > 0;
        }
    }
}
