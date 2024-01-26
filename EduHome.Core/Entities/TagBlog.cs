using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class TagBlog: BaseEntity
    {
    public TagOfBlog TagOfBlog { get; set; }

    public int TagIdOfBlog { get; set; }

     public Blog Blog { get; set; }

     public int BlogId { get; set; }



    }
}
