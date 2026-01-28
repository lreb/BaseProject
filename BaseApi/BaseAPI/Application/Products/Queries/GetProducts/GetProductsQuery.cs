using AutoMapper;
using BaseAPI.Application.Common.Interfaces;
using BaseAPI.Application.Common.Models;
using BaseAPI.Application.Products.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseAPI.Application.Products.Queries.GetProducts;

public record GetProductsQuery : IRequest<Result<IEnumerable<ProductDto>>>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProductDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Set<Domain.Entities.Product>()
            .ToListAsync(cancellationToken);

        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        return Result<IEnumerable<ProductDto>>.SuccessResult(productDtos, "Products retrieved successfully");
    }
}
