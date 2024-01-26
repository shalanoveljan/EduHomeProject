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
    public class EventService : IEventService
    {
        readonly IWebHostEnvironment _env;
        readonly IEventRepository _eventRepository;

        public EventService(IWebHostEnvironment env, IEventRepository eventRepository)
        {
            _env = env;
            _eventRepository = eventRepository;
        }

        public async Task<CommonResponse> CreateAsync(EventPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;

            Event Event = new Event
            {
                JobTime = dto.JobTime,
                Description = dto.Description,
                Title = dto.Title,
                EndDate = dto.EndDate,
                CategoryIdOfEvent = dto.CategoryIdOfEvent,
                SpeakersEvent = new List<SpeakerEvent>(),
                TagsEvent = new List<TagEvent>()
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
            Event.Storage = "wwwroot";
            Event.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/event");

            foreach (var item in dto.TagsIdsOfEvent)
            {
                Event.TagsEvent.Add(new TagEvent
                {
                    Event = Event,
                    TagIdOfEvent = item,
                });
            }

            foreach (var item in dto.SpeakersIds)
            {
                Event.SpeakersEvent.Add(new SpeakerEvent
                {
                    Event = Event,
                    SpeakerId = item,
                });
            }

            await _eventRepository.AddAsync(Event);
            await _eventRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<EventGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<EventGetDto> pagginatedResponse = new PagginatedResponse<EventGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _eventRepository.GetQuery(x => !x.IsDeleted)
             .AsNoTrackingWithIdentityResolution()
             .Include(x => x.SpeakersEvent)
             .ThenInclude(x => x.Speaker)
             .ThenInclude(x=>x.Position)
             .Include(x => x.CategoryOfEvent)
             .Include(x=>x.TagsEvent)
             .ThenInclude(x=>x.TagOfEvent);

            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                  .Select(x =>
              new EventGetDto
              {
                  Title = x.Title,
                  Id = x.Id,
                  Description = x.Description,
                  TagsOfEvent = x.TagsEvent.Select(y => new TagOfEventGetDto { Name = y.TagOfEvent.Name, Id = y.TagIdOfEvent }),
                  Image = x.Image,
                  EndDate = x.EndDate,
                  JobTime= x.JobTime,
                  CreatedAt= x.CreatedAt,
                  Category = new CategoryOfEventGetDto { Name = x.CategoryOfEvent.Name, Id = x.CategoryOfEvent.Id, EventCount = x.CategoryOfEvent.Events.Where(x => !x.IsDeleted).Count() },
                  Speakers = x.SpeakersEvent.Select(y=>new SpeakerGetDto { 
                      FullName=y.Speaker.FullName, 
                      Image=y.Speaker.Image,
                      Position = new PositionGetDto { Name = y.Speaker.Position.Name },
                      Id=y.SpeakerId }),
              })
                .ToListAsync();
            return pagginatedResponse;
        }

        //public static PositionGetDto ConvertToPositionGetDto(PositionOfSpeaker position)
        //{
        //    return new PositionGetDto
        //    {
        //        Name = position.Name,
        //    };
        //}
        
        public async Task<EventGetDto> GetAsync(int id)
        {
            Event? Event = await _eventRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagsEvent.TagOfEvent", "SpeakersEvent.Speaker.Position", "CategoryOfEvent");

            if (Event == null)
            {
                throw new ItemNotFoundException("Event Not Found");
            }

            EventGetDto EventGetDto= new EventGetDto
            {
                
                Title = Event.Title,
                Id = Event.Id,
                Description = Event.Description,
                TagsOfEvent = Event.TagsEvent.Select(y => new TagOfEventGetDto { Name = y.TagOfEvent.Name, Id = y.TagIdOfEvent }),
                Image = Event.Image,
                EndDate = Event.EndDate,
                JobTime = Event.JobTime,
                CreatedAt= Event.CreatedAt,
                Category=new CategoryOfEventGetDto { Name=Event.CategoryOfEvent.Name,Id=Event.CategoryOfEvent.Id,EventCount = Event.CategoryOfEvent.Events.Where(x => !x.IsDeleted).Count() },
                Speakers = Event.SpeakersEvent.Select(y => new SpeakerGetDto { FullName = y.Speaker.FullName, Image = y.Speaker.Image, Position = new PositionGetDto { Name = y.Speaker.Position.Name} }),
            };
            //ConvertToPositionGetDto(y.Speaker.Position)
            return EventGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Event? Event = await _eventRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagsEvent.TagOfEvent", "SpeakersEvent.Speaker", "CategoryOfEvent");

            if (Event == null)
            {
                throw new ItemNotFoundException("Event Not Found");
            }
            Event.IsDeleted = true;
            await _eventRepository.UpdateAsync(Event);
            await _eventRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, EventPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse.StatusCode = 200;
            Event? Event = await _eventRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagsEvent.TagOfEvent", "SpeakersEvent.Speaker.Position", "CategoryOfEvent");

            if (Event == null)
            {
                throw new ItemNotFoundException("Event Not Found");
            }
            Event.Title = dto.Title;
            Event.Description = dto.Description;
            Event.CategoryIdOfEvent = dto.CategoryIdOfEvent;
            Event.JobTime = dto.JobTime;
            Event.EndDate = dto.EndDate;
            Event.CategoryIdOfEvent = dto.CategoryIdOfEvent;
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
                Event.Storage = "wwwroot";
                Event.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/event");
            }
            Event.TagsEvent.Clear();
            Event.SpeakersEvent.Clear();

            foreach (var item in dto.TagsIdsOfEvent)
            {
                Event.TagsEvent.Add(new TagEvent
                {
                    Event = Event,
                    TagIdOfEvent = item,
                });
            }

            foreach (var item in dto.SpeakersIds)
            {
                Event.SpeakersEvent.Add(new SpeakerEvent
                {
                    Event = Event,
                    SpeakerId = item,
                });
            }

            await _eventRepository.UpdateAsync(Event);
            await _eventRepository.SaveChangesAsync();
            return commonResponse;
        }
    }
}
