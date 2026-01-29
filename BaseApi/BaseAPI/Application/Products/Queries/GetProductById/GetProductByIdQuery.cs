using AutoMapper;
using BaseAPI.Application.Common.Exceptions;
using BaseAPI.Application.Common.Interfaces;
using BaseAPI.Application.Common.Models;
using BaseAPI.Application.Products.DTOs;
using MediatR;

namespace BaseAPI.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto>>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IRepository<Domain.Entities.Product> _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IRepository<Domain.Entities.Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);
        }

        var productDto = _mapper.Map<ProductDto>(product);

        return Result<ProductDto>.SuccessResult(productDto, "Product retrieved successfully");
    }
}
