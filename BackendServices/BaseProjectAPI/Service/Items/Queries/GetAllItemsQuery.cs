using BaseProjectAPI.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Items.Queries
{
    /// <summary>
    /// Handle query all records
    /// </summary>
    public class GetAllItemsQuery : IRequest<IEnumerable<Item>>
    {
        /// <summary>
        /// Request handler function
        /// </summary>
        public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<Item>>
        {
            /// <summary>
            /// Provider service
            /// </summary>
            private readonly IItemsService _ItemService;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="ItemService"><see cref="IItemsService"/></param>
            public GetAllItemsQueryHandler(IItemsService ItemService)
            {
                _ItemService = ItemService;
            }

            public async Task<IEnumerable<Item>> Handle(GetAllItemsQuery query, CancellationToken cancellationToken)
            {
                return await _ItemService.GetItemsList();
            }
        }
    }
}
