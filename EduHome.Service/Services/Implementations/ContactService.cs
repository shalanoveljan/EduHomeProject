using AutoMapper;
using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Service.Services.Interfaces;
using Karma.Service.Exceptions;
using Karma.Service.Responses;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Service.Services.Implementations
{
    public class ContactService : IContactService
    {
        readonly IContactRepository _ContactRepository;
        readonly IMapper _mapper;

        public ContactService(IContactRepository ContactRepository, IMapper mapper)
        {
            _ContactRepository = ContactRepository;
            _mapper = mapper;
        }
        public async Task<CommonResponse> CreateAsync(ContactPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };
            Contact contact = _mapper.Map<Contact>(dto);

            //if(contact == null)
            //{
            //    commonResponse.StatusCode= 400;
            //    return commonResponse;
            //}

            await _ContactRepository.AddAsync(contact);
            await _ContactRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<ContactGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<ContactGetDto> pagginatedResponse = new PagginatedResponse<ContactGetDto>();
            pagginatedResponse.CurrentPage=page;
            var query = _ContactRepository.GetQuery(x => x.IsDeleted == false);

            pagginatedResponse.TotalPages= (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
             .Take(3)
              .Select(x => new  ContactGetDto {  Email = x.Email, Name=x.Name, Subject=x.Subject,Text=x.Text, Id = x.Id })
             .ToListAsync();
            return pagginatedResponse;
        }

        public async Task RemoveAsync(int id)
        {
            Contact? Contact = await _ContactRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Contact == null)
            {
                throw new ItemNotFoundException("Contact Not Found");
            }
            Contact.IsDeleted = true;
            await _ContactRepository.UpdateAsync(Contact);
            await _ContactRepository.SaveChangesAsync();
        }
    }
}
