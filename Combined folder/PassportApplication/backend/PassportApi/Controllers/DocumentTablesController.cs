using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class DocumentTablesController : ControllerBase
    {
        private readonly IRootRepository<DocumentTable> _rootRepo;
        private readonly IMapper _mapper;

        public DocumentTablesController(IRootRepository<DocumentTable> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/DocumentTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentTableDTO>>> GetDocumentTables()
        {
            try
            {
                var documentTables = await _rootRepo.GetAllAsync();
                var documentTableDtos = _mapper.Map<IEnumerable<DocumentTableDTO>>(documentTables);
                return Ok(documentTableDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the document tables.");
            }
        }

        // GET: api/DocumentTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentTableDTO>> GetDocumentTable(int id)
        {
            try
            {
                var documentTable = await _rootRepo.GetByIdAsync(id);
                if (documentTable == null)
                {
                    return NotFound($"Document table with ID {id} not found.");
                }

                var documentTableDto = _mapper.Map<DocumentTableDTO>(documentTable);
                return Ok(documentTableDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the document table.");
            }
        }

        // PUT: api/DocumentTables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentTable(int id, DocumentTableDTO documentTableDto)
        {
            if (id != documentTableDto.DocumentTableId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var documentTable = _mapper.Map<DocumentTable>(documentTableDto);
                await _rootRepo.UpdateAsync(id, documentTable);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Document table with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the document table.");
            }
        }
/*
        // POST: api/DocumentTables
        [HttpPost("save-new-form")]
        [RequestSizeLimit(104857600)] // Set limit to 100 MB
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<ActionResult<DocumentTableDTO>> PostDocumentTable([FromForm] IFormFile aadharCard, [FromForm] IFormFile pancard, [FromForm] IFormFile dobProof, [FromForm] IFormFile photo)
        {
            if ((aadharCard == null || aadharCard.Length == 0) ||
                (pancard == null || pancard.Length == 0) ||
                (dobProof == null || dobProof.Length == 0) ||
                (photo == null || photo.Length == 0))
            {
                return BadRequest("One or more files are missing.");
            }

            try
            {
                var documentTable = new DocumentTable();

                using (var memoryStream = new MemoryStream())
                {
                    await aadharCard.CopyToAsync(memoryStream);
                    documentTable.AadharCard = memoryStream.ToArray();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await pancard.CopyToAsync(memoryStream);
                    documentTable.Pancard = memoryStream.ToArray();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await dobProof.CopyToAsync(memoryStream);
                    documentTable.Signature = memoryStream.ToArray();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await photo.CopyToAsync(memoryStream);
                    documentTable.Photo = memoryStream.ToArray();
                }

                await _rootRepo.AddAsync(documentTable);

                return CreatedAtAction(nameof(GetDocumentTable), new { id = documentTable.DocumentTableId }, documentTable);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the document table.");
            }

        }

        [HttpPost("save-renewal-form")]
        [RequestSizeLimit(104857600)] // Set limit to 100 MB
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<ActionResult<DocumentTableDTO>> PostDocumentTable([FromForm] IFormFile aadharCard, [FromForm] IFormFile pancard, [FromForm] IFormFile dobProof, [FromForm] IFormFile photo, [FromForm] IFormFile recentPassport)
        {
            if ((aadharCard == null || aadharCard.Length == 0) ||
                (pancard == null || pancard.Length == 0) ||
                (dobProof == null || dobProof.Length == 0) ||
                (photo == null || photo.Length == 0) ||
                (recentPassport==null || recentPassport.Length == 0))
            {
                return BadRequest("One or more files are missing.");
            }

            try
            {
                var documentTable = new DocumentTable();

                using (var memoryStream = new MemoryStream())
                {
                    await aadharCard.CopyToAsync(memoryStream);
                    documentTable.AadharCard = memoryStream.ToArray();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await pancard.CopyToAsync(memoryStream);
                    documentTable.Pancard = memoryStream.ToArray();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await dobProof.CopyToAsync(memoryStream);
                    documentTable.Signature = memoryStream.ToArray();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await photo.CopyToAsync(memoryStream);
                    documentTable.Photo = memoryStream.ToArray();
                }

                using(var memoryStream = new MemoryStream())
                {
                    await recentPassport.CopyToAsync(memoryStream);
                    documentTable.RecentPassport=memoryStream.ToArray();
                }

                await _rootRepo.AddAsync(documentTable);

                return CreatedAtAction(nameof(GetDocumentTable), new { id = documentTable.DocumentTableId }, documentTable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the document table.");
            }

        }
*/
        // DELETE: api/DocumentTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentTable(int id)
        {
            try
            {
                var documentTable = await _rootRepo.GetByIdAsync(id);
                if (documentTable == null)
                {
                    return NotFound($"Document table with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the document table.");
            }
        }
    }
}
