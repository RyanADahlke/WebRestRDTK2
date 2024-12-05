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
    public class ProductStatusesController : ControllerBase, iController<ProductStatus, ProductStatusDTO>
    {
        private readonly WebRestOracleContext _context;
        private readonly IMapper _mapper;

        public ProductStatusesController(WebRestOracleContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           // _context.LoggedInUserId = "XYZ";
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> Get()
        {
            return await _context.ProductStatuses.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductStatus>> Get(string id)
        {
            var ProductStatus = await _context.ProductStatuses.FindAsync(id);

            if (ProductStatus == null)
            {
                return NotFound();
            }

            return ProductStatus;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductStatusDTO _ProductStatusDTO)
        {

            if (id != _ProductStatusDTO.ProductStatusId)
            {
                return BadRequest();
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //  Set context
                //_context.SetUserID(_context.LoggedInUserId);

                //  POJO code goes here                
                var _item = _mapper.Map<ProductStatus>(_ProductStatusDTO);
                _context.ProductStatuses.Update(_item);
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
        public async Task<ActionResult<ProductStatus>> Post(ProductStatusDTO _ProductStatusDTO)
        {
            ProductStatus _item = _mapper.Map<ProductStatus>(_ProductStatusDTO);
            _item.ProductStatusId = null;      //  Force a new PK to be created
            _context.ProductStatuses.Add(_item);
            await _context.SaveChangesAsync();

            CreatedAtActionResult ret = CreatedAtAction("Get", new { id = _item.ProductStatusId }, _item);
            return Ok(ret);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ProductStatus = await _context.ProductStatuses.FindAsync(id);
            if (ProductStatus == null)
            {
                return NotFound();
            }

            _context.ProductStatuses.Remove(ProductStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }


    }
}
