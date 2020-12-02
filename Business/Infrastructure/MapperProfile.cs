using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Business
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<Customer, CustomerDto>().ReverseMap();
		}
	}
}
