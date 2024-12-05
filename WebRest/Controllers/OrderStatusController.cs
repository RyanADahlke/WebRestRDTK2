using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;
using WebRest.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.ConstrainedExecution;
using WebRestShared.DTO;
namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusesController : ControllerBase, iController<OrderStatus, OrderStatusDTO>
    {
        private readonly WebRestOracleContext _context;
        private readonly IMapper _mapper;

        public OrderStatusesController(WebRestOracleContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           // _context.LoggedInUserId = "XYZ";
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> Get()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrderStatus>> Get(string id)
        {
            var OrderStatus = await _context.OrderStatuses.FindAsync(id);

            if (OrderStatus == null)
            {
                return NotFound();
            }

            return OrderStatus;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderStatusDTO _OrderStatusDTO)
        {

            if (id != _OrderStatusDTO.OrderStatusId)
            {
                return BadRequest();
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //  Set context
                //_context.SetUserID(_context.LoggedInUserId);

                //  POJO code goes here                
                var _item = _mapper.Map<OrderStatus>(_OrderStatusDTO);
                _context.OrderStatuses.Update(_item);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message, e);
            }

            return NoContent();

        }

        [HttpPost]
        public async Task<ActionResult<OrderStatus>> Post(OrderStatusDTO _OrderStatusDTO)
        {
            OrderStatus _item = _mapper.Map<OrderStatus>(_OrderStatusDTO);
            _item.OrderStatusId = null;      //  Force a new PK to be created
            _context.OrderStatuses.Add(_item);
            await _context.SaveChangesAsync();

            CreatedAtActionResult ret = CreatedAtAction("Get", new { id = _item.OrderStatusId }, _item);
            return Ok(ret);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var OrderStatus = await _context.OrderStatuses.FindAsync(id);
            if (OrderStatus == null)
            {
                return NotFound();
            }

            _context.OrderStatuses.Remove(OrderStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.OrderStatuses.Any(e => e.OrderStatusId == id);
        }


    }
}
