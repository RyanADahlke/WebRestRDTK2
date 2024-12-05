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
    public class ProductPricesController : ControllerBase, iController<ProductPrice, ProductPriceDTO>
    {
        private readonly WebRestOracleContext _context;
        private readonly IMapper _mapper;

        public ProductPricesController(WebRestOracleContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           // _context.LoggedInUserId = "XYZ";
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPrice>>> Get()
        {
            return await _context.ProductPrices.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductPrice>> Get(string id)
        {
            var ProductPrice = await _context.ProductPrices.FindAsync(id);

            if (ProductPrice == null)
            {
                return NotFound();
            }

            return ProductPrice;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductPriceDTO _ProductPriceDTO)
        {

            if (id != _ProductPriceDTO.ProductPriceId)
            {
                return BadRequest();
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //  Set context
                //_context.SetUserID(_context.LoggedInUserId);

                //  POJO code goes here                
                var _item = _mapper.Map<ProductPrice>(_ProductPriceDTO);
                _context.ProductPrices.Update(_item);
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
        public async Task<ActionResult<ProductPrice>> Post(ProductPriceDTO _ProductPriceDTO)
        {
            ProductPrice _item = _mapper.Map<ProductPrice>(_ProductPriceDTO);
            _item.ProductPriceId = null;      //  Force a new PK to be created
            _context.ProductPrices.Add(_item);
            await _context.SaveChangesAsync();

            CreatedAtActionResult ret = CreatedAtAction("Get", new { id = _item.ProductPriceId }, _item);
            return Ok(ret);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ProductPrice = await _context.ProductPrices.FindAsync(id);
            if (ProductPrice == null)
            {
                return NotFound();
            }

            _context.ProductPrices.Remove(ProductPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.ProductPrices.Any(e => e.ProductPriceId == id);
        }


    }
}
