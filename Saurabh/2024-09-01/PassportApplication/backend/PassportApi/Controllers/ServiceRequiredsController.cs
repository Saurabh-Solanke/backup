using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportApi.Data;
using PassportApi.Models;
using PassportApi.interfaces;
using PassportApi.Repositories;
using PassportApi.Dtos.ApplicationForm;
using AutoMapper;

namespace PassportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequiredsController : ControllerBase
    {
        private readonly IRootRepository<ServiceRequired> _rootRepo;
        private readonly IMapper _mapper;

        public ServiceRequiredsController(IRootRepository<ServiceRequired> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/ServiceRequireds
        [HttpGet]
        public async Task<ActionResult<ICollection<ServiceRequiredDTO>>> GetServiceRequireds()
        {
            try
            {
                var serviceReq = await _rootRepo.GetAllAsync();
                var serviceReqDtos = _mapper.Map<ICollection<ServiceRequiredDTO>>(serviceReq);
                return Ok(serviceReqDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the service required list.");
            }
        }

        // GET: api/ServiceRequireds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequiredDTO>> GetServiceRequired(int id)
        {
            try
            {
                var serviceRequired = await _rootRepo.GetByIdAsync(id);

                if (serviceRequired == null)
                {
                    return NotFound($"Service required with ID {id} not found.");
                }

                var serviceRequiredDto = _mapper.Map<ServiceRequiredDTO>(serviceRequired);
                return Ok(serviceRequiredDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the service required.");
            }
        }

        // PUT: api/ServiceRequireds/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRequired(int id, ServiceRequiredDTO serviceRequiredDto)
        {
            if (id != serviceRequiredDto.ServiceRequiredId)
            {
                return BadRequest("ID mismatch.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var serviceRequired = _mapper.Map<ServiceRequired>(serviceRequiredDto);
                await _rootRepo.UpdateAsync(id, serviceRequired);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Service required with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the service required.");
            }
        }

        // POST: api/ServiceRequireds
        [HttpPost]
        public async Task<ActionResult<ServiceRequiredDTO>> PostServiceRequired(ServiceRequiredDTO serviceRequiredDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var serviceReq = _mapper.Map<ServiceRequired>(serviceRequiredDto);
                var newServiceReq = await _rootRepo.AddAsync(serviceReq);
                var newServiceReqDto = _mapper.Map<ServiceRequiredDTO>(newServiceReq);

                return CreatedAtAction(nameof(GetServiceRequired), new { id = newServiceReq.ServiceRequiredId }, newServiceReqDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the service required.");
            }
        }

        // DELETE: api/ServiceRequireds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRequired(int id)
        {
            try
            {
                var serviceRequired = await _rootRepo.GetByIdAsync(id);
                if (serviceRequired == null)
                {
                    return NotFound($"Service required with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the service required.");
            }
        }

    }
}
