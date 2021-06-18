using AutoMapper;
using BaseProjectAPI.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Items.Commands
{
    public class UpdateItemCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsEnabled { get; set; }

        public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, int>
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
            public UpdateItemCommandHandler(IItemsService ItemService, IMapper mapper)
            {
                _ItemService = ItemService;
                _mapper = mapper;
            }

            public async Task<int> Handle(UpdateItemCommand command, CancellationToken cancellationToken)
            {
                var itemOnDb = await _ItemService.GetItemById(command.Id);
                if (itemOnDb == null)
                    return default;

                var item = _mapper.Map<Item>(command);
                item.CreatedOn = itemOnDb.CreatedOn;
                item.DisabledOn = !command.IsEnabled ? DateTime.Now : itemOnDb.DisabledOn;

                return await _ItemService.UpdateItem(item);
            }
        }
    }
}
