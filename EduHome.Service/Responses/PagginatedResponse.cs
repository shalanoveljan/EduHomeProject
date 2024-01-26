﻿using System;
namespace Karma.Service.Responses
{
	public class PagginatedResponse<T>
	{
		public List<T> Items { get; set; }
		public int CurrentPage { get; set; }
		public decimal TotalPages { get; set; }
	}
}

