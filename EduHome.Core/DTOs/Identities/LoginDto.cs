using System;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.DTOs
{
	public class LoginDto
	{
		public string UserNameOrEmail { get; set; } = null!;
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		public bool RememberMe { get; set; }
    }
}

