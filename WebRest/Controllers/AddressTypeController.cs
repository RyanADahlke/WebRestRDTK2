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
    public class AddressTypeController : ControllerBase, iController<AddressType, AddressTypeDTO>
    {
        private readonly WebRestOracleContext _context;
        private readonly IMapper _mapper;

        public AddressTypeController(WebRestOracleContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            // _context.LoggedInUserId = "XYZ";
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> Get()
        {
            return await _context.AddressTypes.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AddressType>> Get(string id)
        {
            var _item = await _context.AddressTypes.FindAsync(id);

            if (_item == null)
            {
                return NotFound();
            }

            return _item;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, AddressTypeDTO _AddressTypeDTO)
        {

            if (id != _AddressTypeDTO.AddressTypeId)
            {
                return BadRequest();
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //  Set context
                //_context.SetUserID(_context.LoggedInUserId);

                //  POJO code goes here                
                var _item = _mapper.Map<AddressType>(_AddressTypeDTO);
                _context.AddressTypes.Update(_item);
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
        public async Task<ActionResult<AddressType>> Post(AddressTypeDTO _AddressTypeDTO)
        {
            var _item = _mapper.Map<AddressType>(_AddressTypeDTO);
            _item.AddressTypeId = null;      //  Force a new PK to be created
            _context.AddressTypes.Add(_item);
            await _context.SaveChangesAsync();

            CreatedAtActionResult ret = CreatedAtAction("Get", new { id = _item.AddressTypeId }, _item);
            return Ok(ret);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var _item = await _context.AddressTypes.FindAsync(id);
            if (_item == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(_item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.AddressTypes.Any(e => e.AddressTypeId == id);
        }


    }
}
