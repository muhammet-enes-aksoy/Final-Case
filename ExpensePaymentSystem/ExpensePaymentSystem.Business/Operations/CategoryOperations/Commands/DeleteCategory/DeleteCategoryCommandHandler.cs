using System.Data.Entity;
using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Schema;
using MediatR;


namespace ExpensePaymentSystem.Business.Operations.CategoryOperations.Commands.DeleteCategory;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext context;
    private readonly IMapper mapper;

    public DeleteCategoryCommandHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var Category = await context.Categorys
            .FindAsync(request.Id, cancellationToken);

        if (Category == null)
            return new ApiResponse(CategoryMessages.RecordNotExists);

        Category.IsActive = false;
        await context.SaveChangesAsync(cancellationToken);

        return new ApiResponse("Category deleted!");
    }
}