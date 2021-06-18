using AutoMapper;
using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Domain.ViewModels;
using BaseProjectAPI.Service.Items.Validations;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Items.Commands
{
    public class CreateItemCommand : IRequest<ItemViewModel>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsEnabled { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset DisabledOn { get; set; }

        public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemViewModel>
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
            public CreateItemCommandHandler(IItemsService ItemService, IMapper mapper)
            {
                _ItemService = ItemService;
                _mapper = mapper;
            }

            public async Task<ItemViewModel> Handle(CreateItemCommand command, CancellationToken cancellationToken)
            {
                var item = _mapper.Map<Item>(command);
                item.IsEnabled = true;
                item.CreatedOn = DateTime.Now;
                item.DisabledOn = DateTime.Now;

                var createdItem = await _ItemService.CreateItem(item);

                ItemViewModel itemViewModel = _mapper.Map<ItemViewModel>(createdItem);

                if (itemViewModel is not null)
                    itemViewModel.StockStatus = ItemFilters.GetStockStatus(itemViewModel.Quantity);

                return itemViewModel;
            }
        }
    }
}
