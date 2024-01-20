using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.SystemUserOperations;

public class SystemUserCommandHandler :
    IRequestHandler<CreateSystemUserCommand, ApiResponse<SystemUserResponse>>,
    IRequestHandler<UpdateSystemUserCommand,ApiResponse>,
    IRequestHandler<DeleteSystemUserCommand,ApiResponse>

{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public SystemUserCommandHandler(ExpensePaymentSystemDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<SystemUserResponse>> Handle(CreateSystemUserCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await dbContext.Set<SystemUser>().Where(x => x.UserName == request.Model.UserName)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity != null)
        {
            return new ApiResponse<SystemUserResponse>($"{request.Model.UserName} is in use.");
        }
        
        var entity = mapper.Map<SystemUserRequest, SystemUser>(request.Model);
        
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<SystemUser, SystemUserResponse>(entityResult.Entity);
        return new ApiResponse<SystemUserResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateSystemUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<SystemUser>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        fromdb.Email = request.Model.Email;
        fromdb.Role = request.Model.Role;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteSystemUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<SystemUser>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}