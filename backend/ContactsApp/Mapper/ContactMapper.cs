using AutoMapper;
using ContactsApp.DTO;
using ContactsApp.Models;

namespace ContactsApp.Mapper
{
    public class ContactMapper : Profile
    {
        public ContactMapper()
        {
            CreateMap<CreateContactRequest, Contact>();
            CreateMap<Contact, GetContactResult>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category != null ? new GetCategoryResult { Id = src.Category.Id, Name = src.Category.Name } : null))
                .ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src => src.Subcategory != null ? new GetSubcategoryResult { Id = src.Subcategory.Id, Name = src.Subcategory.Name } : null));

            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<Category, GetCategoryResult>();

            CreateMap<CreateSubcategoryRequest, Subcategory>();
            CreateMap<Subcategory, GetSubcategoryResult>();
        }

    }
}
