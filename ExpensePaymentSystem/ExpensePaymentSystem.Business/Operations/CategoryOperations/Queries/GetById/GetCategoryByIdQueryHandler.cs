using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.CategoryOperations.Queries.GetById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryResponse>>
{
    private readonly ExpensePaymentSystemDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categorys
            .Where(x => x.IsActive)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (category == null)
            return new ApiResponse<CategoryResponse>(CategoryMessages.RecordNotExists);

        var response = _mapper.Map<CategoryResponse>(category);

        return new ApiResponse<CategoryResponse>(response);
    }
}