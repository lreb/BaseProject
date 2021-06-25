using BaseProjectAPI.Domain.Helpers;
using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Domain.ViewModels;
using BaseProjectAPI.Service.Items.Commands;
using BaseProjectAPI.Service.Items.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        /// <summary>
        /// Mediator service
        /// </summary>
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns><see cref="IEnumerable"/> of <see cref="ItemViewModel"/></returns>
        [HttpGet]
        public async Task<IActionResult> GetItems(CancellationToken cancellationToken) => Ok(await _mediator.Send(new GetAllItemsQuery(), cancellationToken));

        /// <summary>
        /// Retrieves item by id
        /// </summary>
        /// <param name="id"><see cref="int"/>item id</param>
        /// <param name="cancellationToken">client cancellation request</param>
        /// <returns><see cref="ItemViewModel"/></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id, CancellationToken cancellationToken)
        {
            var item = await _mediator.Send(new GetItemByIdQuery() { Id = id }, cancellationToken);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Updates an item
        /// </summary>
        /// <param name="id">item id</param>
        /// <param name="item">item data to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(long id, UpdateItemCommand item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            try
            {
                var itemUpdated = await _mediator.Send(item);
            }
            catch (Exception ex)
            {
                throw new BaseException($"Cannot update {nameof(Item)}", ex);
            }

            return NoContent();
        }

        /// <summary>
        /// Create anew item
        /// </summary>
        /// <param name="item"><see cref="CreateItemCommand"/></param>
        /// <returns>Redirection to retrieve item created</returns>
        [HttpPost]
        public async Task<IActionResult> PostItem(CreateItemCommand item)
        {
            try
            {
                var itemCreated = await _mediator.Send(item);
                // TODO: use Item or ItemViewModel ????
                return RedirectToAction(nameof(GetItem), new { Id = itemCreated.Id });
                // return CreatedAtAction("GetItem", new { id = c.Id }, c);
            }
            catch (Exception ex)
            {

                throw new BaseException($"Cannot save {nameof(Item)}", ex);
            }
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id">item id to delete</param>
        /// <returns><see cref="NoContentResult"/></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var itemDeleted = await _mediator.Send(new DeleteItemCommand() { Id = id });
            }
            catch (Exception ex)
            {
                throw new BaseException($"Cannot delete {nameof(Item)}", ex);
            }
            
            return NoContent();
        }
    }
}
