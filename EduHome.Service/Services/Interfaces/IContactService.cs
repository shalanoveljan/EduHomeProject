using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface IContactService
    {
        public Task<PagginatedResponse<ContactGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(ContactPostDto dto);

        public Task RemoveAsync(int id);
    }
}
