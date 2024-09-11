using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportApi.Data;
using PassportApi.Dtos.ApplicationForm;
using PassportApi.interfaces;
using PassportApi.Models;

namespace PassportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressTablesController : ControllerBase
    {
        private readonly IRootRepository<AddressTable> _rootRepo;
        private readonly IMapper _mapper;

        public AddressTablesController(IRootRepository<AddressTable> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/AddressTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressTableDTO>>> GetAddressTables()
        {
            try
            {
                var addressTables = await _rootRepo.GetAllAsync();
                var addressTableDtos = _mapper.Map<IEnumerable<AddressTableDTO>>(addressTables);
                return Ok(addressTableDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the address tables.");
            }
        }

        // GET: api/AddressTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressTableDTO>> GetAddressTable(int id)
        {
            try
            {
                var addressTable = await _rootRepo.GetByIdAsync(id);
                if (addressTable == null)
                {
                    return NotFound($"Address table with ID {id} not found.");
                }

                var addressTableDto = _mapper.Map<AddressTableDTO>(addressTable);
                return Ok(addressTableDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the address table.");
            }
        }

        // PUT: api/AddressTables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressTable(int id, AddressTableDTO addressTableDto)
        {
            if (id != addressTableDto.AddressTableId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var addressTable = _mapper.Map<AddressTable>(addressTableDto);
                await _rootRepo.UpdateAsync(id, addressTable);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Address table with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the address table.");
            }
        }

        // POST: api/AddressTables
        [HttpPost]
        public async Task<ActionResult<AddressTableDTO>> PostAddressTable(AddressTableDTO addressTableDto)
        {
            try
            {
                var addressTable = _mapper.Map<AddressTable>(addressTableDto);
                var newAddressTable = await _rootRepo.AddAsync(addressTable);
                var newAddressTableDto = _mapper.Map<AddressTableDTO>(newAddressTable);

                return CreatedAtAction(nameof(GetAddressTable), new { id = newAddressTableDto.AddressTableId }, newAddressTableDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the address table.");
            }
        }

        // DELETE: api/AddressTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressTable(int id)
        {
            try
            {
                var addressTable = await _rootRepo.GetByIdAsync(id);
                if (addressTable == null)
                {
                    return NotFound($"Address table with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the address table.");
            }
        }

    }
}
