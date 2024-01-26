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
using System.Threading.Tasks.Dataflow;

namespace EduHome.Service.Services.Implementations
{
    public class BoardService : IBoardService
    {
        readonly IBoardRepository _BoardRepository;

        public BoardService(IBoardRepository BoardRepository)
        {
            _BoardRepository = BoardRepository;
        }

        public async Task<CommonResponse> CreateAsync(BoardPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200,
            };
            Board Board = new Board();
            Board.Content = dto.Content;

            
            await _BoardRepository.AddAsync(Board);
            await _BoardRepository.SaveChangesAsync();
            return commonResponse;
        }

        public async Task<PagginatedResponse<BoardGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<BoardGetDto> pagginatedResponse = new PagginatedResponse<BoardGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _BoardRepository.GetQuery(x => !x.IsDeleted);
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 3);

            pagginatedResponse.Items = await query.Skip((page - 1) * 3)
                .Take(3)
                 .Select(x => new BoardGetDto {Content=x.Content, Id = x.Id,CreatedAt=x.CreatedAt })
                .ToListAsync();

            return pagginatedResponse;
        }

        public async Task<BoardGetDto> GetAsync(int id)
        {
            Board? Board = await _BoardRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Board == null)
            {
                throw new ItemNotFoundException("Board Not Found");
            }

            BoardGetDto BoardGetDto = new BoardGetDto
            {
                Content = Board.Content,
                Id= Board.Id,
                CreatedAt= Board.CreatedAt

            };
            return BoardGetDto;
        }

        public async Task RemoveAsync(int id)
        {
            Board? Board = await _BoardRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Board == null)
            {
                throw new ItemNotFoundException("Board Not Found");
            }

            Board.IsDeleted = true;
            await _BoardRepository.UpdateAsync(Board);
            await _BoardRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, BoardPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200,
            };
            Board? Board = await _BoardRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Board == null)
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "Board Not Found";
                return commonResponse;
            }

            Board.Content = dto.Content;
            await _BoardRepository.UpdateAsync(Board);
            await _BoardRepository.SaveChangesAsync();
            return commonResponse;
        }
    }
}
