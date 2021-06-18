using AutoMapper;
using BaseProjectAPI.Domain.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Items.Queries
{
    public class GetItemByIdQuery : IRequest<ItemViewModel>
    {
        /// <summary>
        /// Item parameter id requested
        /// </summary>
        public int Id { get; set; }

        public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemViewModel>
        {
            /// <summary>
            /// Provider service
            /// </summary>
            private readonly IItemsService _ItemService;

            /// <summary>
            /// Auto mapper service
            /// </summary>
            private readonly IMapper _mapper;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="ItemService"><see cref="IItemsService"/></param>
            /// <param name="mapper"><see cref="IMapper"/></param>
            public GetItemByIdQueryHandler(IItemsService ItemService, IMapper mapper)
            {
                _ItemService = ItemService;
                _mapper = mapper;
            }

            public async Task<ItemViewModel> Handle(GetItemByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _ItemService.GetItemById(query.Id, cancellationToken);
                var itemViewModel = _mapper.Map<ItemViewModel>(item);

                if (itemViewModel is not null)
                    itemViewModel.StockStatus = ItemFilters.GetStockStatus(item.Quantity);

                return itemViewModel;
            }
        }
    }
}
