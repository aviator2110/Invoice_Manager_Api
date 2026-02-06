using AutoMapper;
using Invoice_Manager_API.DTO.CustomerDTO;
using Invoice_Manager_API.DTO.InvoiceDTO;
using Invoice_Manager_API.DTO.InvoiceRowDTO;
using Invoice_Manager_API.Models;

namespace Invoice_Manager_API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Customer

        CreateMap<Customer, CustomerResponseDto>();

        CreateMap<CustomerCreateRequest, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        CreateMap<CustomerUpdateRequest, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        // Invoice

        CreateMap<Invoice, InvoiceResponseDto>()
            .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.Rows));

        CreateMap<InvoiceCreateRequest, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalSum, opt => opt.MapFrom(src => src.Rows.Sum(r => r.Sum)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        CreateMap<InvoiceUpdateRequest, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalSum, opt => opt.MapFrom(src => src.Rows.Sum(r => r.Sum)))
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        CreateMap<InvoiceStatusUpdateRequest, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.StartDate, opt => opt.Ignore())
            .ForMember(dest => dest.EndDate, opt => opt.Ignore())
            .ForMember(dest => dest.Rows, opt => opt.Ignore())
            .ForMember(dest => dest.TotalSum, opt => opt.Ignore())
            .ForMember(dest => dest.Comment, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        // InvoiceRow

        CreateMap<InvoiceRow, InvoiceRowResponseDto>();

        CreateMap<InvoiceRowCreateRequest, InvoiceRow>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Quantity * src.Rate));

        CreateMap<InvoiceRowUpdateRequest, InvoiceRow>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Quantity * src.Rate));
    }
}