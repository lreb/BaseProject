using AutoMapper;
using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Domain.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace BaseProjectAPI.Service.Items.Queries
{
    /// <summary>
    /// Handle query all records
    /// </summary>
    public class GetAllItemsQuery : IRequest<IEnumerable<ItemViewModel>>
    {
        /// <summary>
        /// Request handler function
        /// </summary>
        public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<ItemViewModel>>
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
            public GetAllItemsQueryHandler(IItemsService ItemService, IMapper mapper)
            {
                _ItemService = ItemService;
                _mapper = mapper;
            }

            /// <summary>
            /// Handle request for all items
            /// </summary>
            /// <param name="query">Client request object</param>
            /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
            /// <returns></returns>
            public async Task<IEnumerable<ItemViewModel>> Handle(GetAllItemsQuery query, CancellationToken cancellationToken)
            {
                var items = await _ItemService.GetItemsList();
                var itemsViewModel = _mapper.Map<IEnumerable<ItemViewModel>>(items);
                return itemsViewModel;
            }
        }
    }
}
