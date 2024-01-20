using System.Data.Entity;
using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;


namespace ExpensePaymentSystem.Business.Operations.UserOperations.Queries.GetById;
public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetUserByIdCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<UserResponse>(UserMessages.RecordNotExists);
        }
        
        var mapped = mapper.Map<User, UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }

}