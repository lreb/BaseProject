using BaseProjectAPI.Domain.Helpers;
using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Domain.ViewModels;
using BaseProjectAPI.Service.Items.Commands;
using BaseProjectAPI.Service.Items.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace BaseProjectAPI.Controllers
{
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
        public async Task<IActionResult> GetItems() => Ok(await _mediator.Send(new GetAllItemsQuery()));

        /// <summary>
        /// Retrieves item by id
        /// </summary>
        /// <param name="id"><see cref="int"/>item id</param>
        /// <returns><see cref="ItemViewModel"/></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _mediator.Send(new GetItemByIdQuery() { Id = id });

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        //// DELETE: api/Items/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteItem(long id)
        //{
        //    var item = await _context.Items.FindAsync(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Items.Remove(item);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ItemExists(long id)
        //{
        //    return _context.Items.Any(e => e.Id == id);
        //}
    }
}
