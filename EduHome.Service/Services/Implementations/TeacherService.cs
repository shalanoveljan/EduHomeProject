using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Data.Repositories.Implementations;
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
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        readonly ITeacherRepository _teacherRepository;
        readonly IDutyRepository _dutyRepository;
        readonly IDegreeRepository _degreeRepository;
        readonly IWebHostEnvironment _env;

        public TeacherService(ITeacherRepository teacherRepository, IDutyRepository dutyRepository, IDegreeRepository degreeRepository, IWebHostEnvironment env)
        {
            _teacherRepository = teacherRepository;
            _dutyRepository = dutyRepository;
            _degreeRepository = degreeRepository;
            _env = env;
        }
        private async Task<bool> CheckDuty(int id)
        {
            return await _dutyRepository.GetQuery(x => !x.IsDeleted && x.Id == id).CountAsync() > 0;
        }
        public async Task<CommonResponse> CreateAsync(TeacherPostDto dto)
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

            if (dto.Icons == null || dto.Urls == null || dto.Icons.Count() != dto.Urls.Count())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The socihal network is not valid";
                return commonResponse;
            }
           

            if (dto.Icons.Any(x => string.IsNullOrWhiteSpace(x)) || dto.Urls.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The socihal network is not valid";
                return commonResponse;
            }

            if (dto.SkillKeys == null || dto.SkillValues == null || dto.SkillKeys.Count() != dto.SkillValues.Count())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The Skill is not valid";
                return commonResponse;
            }
            if (dto.SkillKeys.Any(x => string.IsNullOrWhiteSpace(x)) || dto.SkillValues.Any(x => !x.HasValue))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The Skill  is not valid";
                return commonResponse;
            }

            if (!await CheckDuty(dto.DutyId))
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "The Duty is not valid";
                return commonResponse;
            }

            Teacher Teacher=new Teacher();
            Teacher.FullName= dto.FullName;
            Teacher.DutyId= dto.DutyId;
            Teacher.Info= dto.Info;
            Teacher.Email= dto.Email;
            Teacher.PhoneNumber= dto.PhoneNumber;
            Teacher.Experience= dto.Experience;
            Teacher.Skype= dto.Skype;
            Teacher.DegreeId= dto.DegreeId;

            Teacher.SocialNetworks = new List<SocialNetwork>();
            Teacher.Skills=new List<Skill>();
            Teacher.FacultyTeachers=new List<FacultyTeacher>();
            Teacher.hobbyTeachers = new List<HobbyTeacher>();


            if (!dto.ImageFile.IsImage())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image is not valid";
                return commonResponse;
            }

            if (dto.ImageFile.IsSizeOk(1))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "Image  size is not valid";
                return commonResponse;
            }

            Teacher.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/teacher");

            for (int i = 0; i < dto.Icons.Count(); i++)
            {
                SocialNetwork socialNetwork = new SocialNetwork
                {
                    Teacher = Teacher,
                    Icon = dto.Icons[i],
                    Url = dto.Urls[i]
                };
                Teacher.SocialNetworks.Add(socialNetwork);
            }

            foreach (var item in dto.FaculitiesIds)
            {
                Teacher.FacultyTeachers.Add(new FacultyTeacher
                {
                    Teacher=Teacher,
                    FacultyId=item
                });
            }

            foreach (var item in dto.HobbiesIds)
            {
                Teacher.hobbyTeachers.Add(new HobbyTeacher
                {
                    Teacher = Teacher,
                    HobbyID = item
                });
            }

            for (int i = 0; i < dto.SkillKeys.Count(); i++)
            {
                Teacher.Skills.Add(new Skill
                {
                   Teacher  = Teacher,
                    Key = dto.SkillKeys[i],
                    Value = dto.SkillValues[i],
                });
            }
            await _teacherRepository.AddAsync(Teacher);
            await _teacherRepository.SaveChangesAsync();
            return commonResponse;


        }

        public async Task<PagginatedResponse<TeacherGetDto>> GetAllAsync(int page = 1)
        {
            PagginatedResponse<TeacherGetDto> pagginatedResponse = new PagginatedResponse<TeacherGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _teacherRepository.GetQuery(x => !x.IsDeleted)
               .AsNoTrackingWithIdentityResolution()
               .Include(x => x.Duty)
               .Include(x => x.Degree)
               .Include(x => x.FacultyTeachers)
               .ThenInclude(x => x.Faculty)
               .Include(x=>x.hobbyTeachers)
               .ThenInclude(x=>x.Hobby);
            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / 6);

            pagginatedResponse.Items = await query.Skip((page - 1) * 6)
               .Take(6)
               .Select(x =>
               new TeacherGetDto
               {
                 Id= x.Id,
                 FullName= x.FullName,
                 Info= x.Info,
                 Experience= x.Experience,
                 Email= x.Email,
                 Skype= x.Skype,
                 Image= x.Image,
                 PhoneNumber= x.PhoneNumber,
                 Duty=new DutyGetDto {Name=x.Duty.Name },
                 Degree=new DegreeGetDto {Name=x.Degree.Name },
                 Faculties = x.FacultyTeachers.Select(y => new FacultyGetDto { Name = y.Faculty.Name, Id = y.FacultyId }),
                 Hobbies = x.hobbyTeachers.Select(y => new HobbyGetDto { Name = y.Hobby.Name, Id = y.HobbyID }),
                   SocialNetworks =x.SocialNetworks
               })
               .ToListAsync();

            return pagginatedResponse;

        }

        public async Task<TeacherGetDto> GetAsync(int id)
        {
            var query = _teacherRepository.GetQuery(x => x.IsDeleted == false && x.Id == id)
             .AsNoTrackingWithIdentityResolution()
               .Include(x => x.Duty)
               .Include(x => x.Degree)
               .Include(x => x.FacultyTeachers)
               .ThenInclude(x => x.Faculty)
               .Include(x=>x.hobbyTeachers)
               .ThenInclude(x=>x.Hobby)
               .Include(x=>x.Skills);


            TeacherGetDto? Teacher = await query.Select(x => new TeacherGetDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Info = x.Info,
                Experience = x.Experience,
                Email = x.Email,
                Skype = x.Skype,
                Image = x.Image,
                PhoneNumber = x.PhoneNumber,
                Icons=x.SocialNetworks.Select(y=>y.Icon).ToList(),
                Urls=x.SocialNetworks.Select(y=>y.Url).ToList(),
                SkillKeys=x.Skills.Select(y=>y.Key).ToList(),
                SkillValues=x.Skills.Select(y=>y.Value).ToList(),
                Duty = new DutyGetDto { Name = x.Duty.Name },
                Degree = new DegreeGetDto { Name = x.Degree.Name },
                Faculties = x.FacultyTeachers.Select(y => new FacultyGetDto { Name = y.Faculty.Name, Id = y.FacultyId }),
                Hobbies = x.hobbyTeachers.Select(y => new HobbyGetDto { Name = y.Hobby.Name, Id = y.HobbyID }),
                SocialNetworks = x.SocialNetworks
            }).FirstOrDefaultAsync();
            if (Teacher == null)
            {
                throw new ItemNotFoundException("Teacher Not Found");
            }

            return Teacher;
        }

        public async Task RemoveAsync(int id)
        {
            Teacher? Teacher = await _teacherRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Teacher == null)
            {
                throw new ItemNotFoundException("Teacher Not Found");
            }
            Teacher.IsDeleted = true;
            await _teacherRepository.UpdateAsync(Teacher);
            await _teacherRepository.SaveChangesAsync();
        }

        public async Task<CommonResponse> UpdateAsync(int id, TeacherPostDto dto)
        {
            CommonResponse commonResponse = new CommonResponse
            {
                StatusCode = 200
            };

            //if (dto.ImageFile == null)
            //{
            //    commonResponse.StatusCode = 400;
            //    commonResponse.Message = "The field image is required";
            //    return commonResponse;
            //}

            if (dto.Icons == null || dto.Urls == null || dto.Icons.Count() != dto.Urls.Count())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The socihal network is not valid";
                return commonResponse;
            }


            if (dto.Icons.Any(x => string.IsNullOrWhiteSpace(x)) || dto.Urls.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The socihal network is not valid";
                return commonResponse;
            }

            if (dto.SkillKeys == null || dto.SkillValues == null || dto.SkillKeys.Count() != dto.SkillValues.Count())
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The Skill is not valid";
                return commonResponse;
            }
            if (dto.SkillKeys.Any(x => string.IsNullOrWhiteSpace(x)) || dto.SkillValues.Any(x => !x.HasValue))
            {
                commonResponse.StatusCode = 400;
                commonResponse.Message = "The Skill Value is not valid";
                return commonResponse;
            }

            if (!await CheckDuty(dto.DutyId))
            {
                commonResponse.StatusCode = 404;
                commonResponse.Message = "The Duty is not valid";
                return commonResponse;
            }

            Teacher? Teacher = await _teacherRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "FacultyTeachers.Faculty","Degree","Duty", "SocialNetworks", "hobbyTeachers.Hobby", "Skills");

            if (Teacher == null)
            {
                throw new ItemNotFoundException("Teacher Not Found");
            }

            Teacher.FullName = dto.FullName;
            Teacher.DutyId = dto.DutyId;
            Teacher.Info = dto.Info;
            Teacher.Email = dto.Email;
            Teacher.PhoneNumber = dto.PhoneNumber;
            Teacher.Experience = dto.Experience;
            Teacher.Skype = dto.Skype;
            Teacher.DegreeId = dto.DegreeId;


            if (dto.ImageFile!=null)
            {
                if (!dto.ImageFile.IsImage())
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image is not valid";
                    return commonResponse;
                }

                if (dto.ImageFile.IsSizeOk(1))
                {
                    commonResponse.StatusCode = 400;
                    commonResponse.Message = "Image  size is not valid";
                    return commonResponse;
                }
                Helper.RmoveFile(_env.WebRootPath, "assets/img/teacher", Teacher.Image);
            Teacher.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/teacher");
            }
            else
            {

            }
         


            Teacher.SocialNetworks.Clear();

            for (int i = 0; i < dto.Icons.Count(); i++)
            {
                SocialNetwork socialNetwork = new SocialNetwork
                {
                    Teacher = Teacher,
                    Icon = dto.Icons[i],
                    Url = dto.Urls[i]
                };
                Teacher.SocialNetworks.Add(socialNetwork);
            }


            Teacher.FacultyTeachers.Clear();

            foreach (var item in dto.FaculitiesIds)
            {
                Teacher.FacultyTeachers.Add(new FacultyTeacher
                {
                    Teacher = Teacher,
                    FacultyId = item
                });
            }
            Teacher.hobbyTeachers.Clear();
            foreach (var item in dto.HobbiesIds)
            {
                Teacher.hobbyTeachers.Add(new HobbyTeacher
                {
                    Teacher = Teacher,
                    HobbyID = item
                });
            }

            Teacher.Skills.Clear();

            for (int i = 0; i < dto.SkillKeys.Count(); i++)
            {
                Teacher.Skills.Add(new Skill
                {
                    Teacher = Teacher,
                    Key = dto.SkillKeys[i],
                    Value = dto.SkillValues[i],
                });
            }
            await _teacherRepository.UpdateAsync(Teacher);
            await _teacherRepository.SaveChangesAsync();

            return commonResponse;


        }
    }
}
