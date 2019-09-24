using AutoMapper;
using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<AccountDto, Account>();
            Mapper.CreateMap<TransactionDto, UpdateTransactionDto>();
            Mapper.CreateMap<TransactionDto, Transaction>();
            Mapper.CreateMap<UpdateTransactionDto, Transaction>();
            Mapper.CreateMap<CategoryDto, Category>();
        }
    }
}
