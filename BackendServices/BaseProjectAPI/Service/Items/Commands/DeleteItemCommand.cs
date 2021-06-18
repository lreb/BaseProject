using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Items.Commands
{
    public class DeleteItemCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, int>
        {
            private readonly IItemsService _ItemService;

            public DeleteItemCommandHandler(IItemsService ItemService)
            {
                _ItemService = ItemService;
            }

            public async Task<int> Handle(DeleteItemCommand command, CancellationToken cancellationToken)
            {
                var itemOnDb = await _ItemService.GetItemById(command.Id);
                if (itemOnDb == null)
                    return default;

                return await _ItemService.DeleteItem(itemOnDb);
            }
        }
    }
}
