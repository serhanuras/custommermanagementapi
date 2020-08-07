using AutoMapper;
using CustomerManagement.API.Dtos.Company;
using CustomerManagement.API.Dtos.Customer;
using CustomerManagement.API.Dtos.Title;

namespace CustomerManagement.API.Dtos
{
    public class AutoMapperProfiles :  Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Entities.Title, TitleUpsertDto>().ReverseMap();
            CreateMap<Entities.Title, TitleFilterDto>().ReverseMap();
            CreateMap<Entities.Title, TitleDto>().ReverseMap();
            
            CreateMap<Entities.Company, CompanyUpsertDto>().ReverseMap();
            CreateMap<Entities.Company, CompanyFilterDto>().ReverseMap();
            CreateMap<Entities.Company, CompanyDto>().ReverseMap();
            
            CreateMap<Entities.Customer, CustomerUpsertDto>().ReverseMap();
            CreateMap<Entities.Customer, CustomerFilterDto>().ReverseMap();
            CreateMap<Entities.Customer, CustomerDto>()
                .ForMember(x => x.CompanyName, options => options.MapFrom(y => y.Company.Name))
                .ForMember(x => x.CompanyId, options => options.MapFrom(y => y.Company.Id))
                .ForMember(x => x.Title, options => options.MapFrom(x => x.Title.Value))
                .ForMember(x => x.TitleId, options => options.MapFrom(x => x.Title.Id));
            
        }
    }
}