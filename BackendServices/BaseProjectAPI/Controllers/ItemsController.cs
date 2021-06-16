﻿using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Service.Items.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaseProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly BaseDataContext _context;

        public ItemsController(
            //BaseDataContext context
            IMediator mediator)
        {
            //_context = context;
            _mediator = mediator;
        }

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns><see cref="Item"/></returns>
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            return Ok(await _mediator.Send(new GetAllItemsQuery()));
        }

        // GET: api/Items/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetItem(long id)
        //{
        //    var item = await _context.Items.FindAsync(id);

        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(item);
        //}

        //// PUT: api/Items/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutItem(long id, Item item)
        //{
        //    if (id != item.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(item).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ItemExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Items
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Item>> PostItem(Item item)
        //{
        //    _context.Items.Add(item);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetItem", new { id = item.Id }, item);
        //}

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
