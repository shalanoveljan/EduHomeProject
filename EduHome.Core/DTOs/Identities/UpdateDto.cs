using System;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.DTOs
{
	public record UpdateDto
	{
		public string UserName { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string Surname { get; set; } = null!;
        [DataType(DataType.Password)]
		public string? OldPassword { get; set; }
        [DataType(DataType.Password)]

        public string? NewPassword { get; set; } 
    }
}

