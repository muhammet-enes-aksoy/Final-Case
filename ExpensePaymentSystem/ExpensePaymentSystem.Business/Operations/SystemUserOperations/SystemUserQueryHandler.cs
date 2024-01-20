using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.SystemUserOperations;

public class SystemUserQueryHandler :
    IRequestHandler<GetAllSystemUserQuery, ApiResponse<List<SystemUserResponse>>>,
    IRequestHandler<GetSystemUserByIdQuery, ApiResponse<SystemUserResponse>>,
    IRequestHandler<GetSystemUserByParameterQuery, ApiResponse<List<SystemUserResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public SystemUserQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<SystemUserResponse>>> Handle(GetAllSystemUserQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<SystemUser>().ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<SystemUser>, List<SystemUserResponse>>(list);
         return new ApiResponse<List<SystemUserResponse>>(mappedList);
    }

    public async Task<ApiResponse<SystemUserResponse>> Handle(GetSystemUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<SystemUser>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<SystemUserResponse>("Record not found");
        }
        
        var mapped = mapper.Map<SystemUser, SystemUserResponse>(entity);
        return new ApiResponse<SystemUserResponse>(mapped);
    }

    public async Task<ApiResponse<List<SystemUserResponse>>> Handle(GetSystemUserByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<SystemUser>(true);
        if (string.IsNullOrEmpty(request.FirstName))
            
            predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
        if (string.IsNullOrEmpty(request.LastName))
            predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
        
        if (string.IsNullOrEmpty(request.UserName))
            predicate.And(x => x.UserName.ToUpper().Contains(request.UserName.ToUpper()));
        
        var list =  await dbContext.Set<SystemUser>()
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<SystemUser>, List<SystemUserResponse>>(list);
        return new ApiResponse<List<SystemUserResponse>>(mappedList);
    }
}