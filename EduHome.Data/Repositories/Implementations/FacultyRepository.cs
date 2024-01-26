using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Data.Repositories.Implementations
{
    public class FacultyRepository:Repository<Faculty>,IFacultyRepository
    {
        public FacultyRepository(EduHomeDbContext context) : base(context)
        {

        }
    }
}
