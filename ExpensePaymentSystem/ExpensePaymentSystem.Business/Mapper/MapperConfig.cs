using AutoMapper;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;


namespace ExpensePaymentSystem.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<AccountRequest, Account>();
        CreateMap<Account, AccountResponse>()
            .ForMember(dest => dest.EmployeeName,
                src => src.MapFrom(x => x.Employee.FirstName + " " + x.Employee.LastName));

        CreateMap<AddressRequest, Address>();
        CreateMap<Address, AddressResponse>()
            .ForMember(dest => dest.EmployeeName,
                src => src.MapFrom(x => x.Employee.FirstName + " " + x.Employee.LastName));

        CreateMap<CategoryRequest, Category>();
        CreateMap<Category, CategoryResponse>();

        CreateMap<ContactRequest, Contact>();
        CreateMap<Contact, ContactResponse>()
            .ForMember(dest => dest.EmployeeName,
                src => src.MapFrom(x => x.Employee.FirstName + " " + x.Employee.LastName));

        CreateMap<ExpenseClaimRequest, ExpenseClaim>();
        CreateMap<ExpenseClaim, ExpenseClaimResponse>()
            .ForMember(dest => dest.Category, 
                opt => opt.MapFrom(src => src.Category.CategoryType.ToString()))
            .ForMember(dest => dest.PaymentMethod, 
                opt => opt.MapFrom(src => src.PaymentMethod.PaymentMethodType.ToString()))
            .ForMember(dest => dest.EmployeeName,
                src => src.MapFrom(x => x.Employee.FirstName + " " + x.Employee.LastName));

        CreateMap<PaymentMethodRequest, PaymentMethod>();
        CreateMap<PaymentMethod, PaymentMethodResponse>();

        CreateMap<EmployeeRequest, Employee>();
        CreateMap<Employee, EmployeeResponse>();


    }
}