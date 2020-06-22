using AutoMapper;

using Xayah.App.ViewModels;
using Xayah.Business.Model;

namespace Xayah.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Document, DocumentViewModel>().ReverseMap();
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
        }
    }
}
