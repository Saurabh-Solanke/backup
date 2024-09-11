using Microsoft.AspNetCore.Mvc;
using PassPortal_API.Services;
using PassPortal_API.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PassPortal_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassportOfficesController : ControllerBase
    {
        private readonly PassportOfficeService _passportOfficeService;

        public PassportOfficesController(PassportOfficeService passportOfficeService)
        {
            _passportOfficeService = passportOfficeService;
        }
        //Upload Passport Offices
      /*  [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPassportOffices([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid CSV file.");

            var result = await _passportOfficeService.ProcessCsvFileAsync(file);

            if (result.IsSuccess)
                return Ok("Data successfully uploaded.");
            else
                return BadRequest(result.ErrorMessage);
        }*/

        //Get All Passport Offices
        [HttpGet]
        public async Task<IActionResult> GetAllPassportOffices()
        {
            var passportOffices = await _passportOfficeService.GetAllPassportOfficesAsync();
            if (passportOffices == null)
                return NotFound("No passport offices found.");
            
            return Ok(passportOffices);
        }
        //Get Passport Office by ID and Edit
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPassportOffice(int id, [FromBody] PassportOfficeDTO passportOfficeDTO)
        {
            if (passportOfficeDTO == null)
                return BadRequest("Invalid data.");

            var result = await _passportOfficeService.UpdatePassportOfficeAsync(id, passportOfficeDTO);

            if (result.IsSuccess)
                return Ok("Passport office updated successfully.");
            else
                return NotFound(result.ErrorMessage);
        }

        //Add Passport Office
        [HttpPost]
        public async Task<IActionResult> CreatePassportOffice([FromBody] PassportOfficeDTO passportOfficeDTO)
        {
            if (passportOfficeDTO == null)
                return BadRequest("Invalid data.");

            var result = await _passportOfficeService.CreatePassportOfficeAsync(passportOfficeDTO);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetAllPassportOffices), new { id = result.CreatedId }, passportOfficeDTO);
            else
                return BadRequest(result.ErrorMessage);
        }

        //Delete Passport Office by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassportOffice(int id)
        {
            var result = await _passportOfficeService.DeletePassportOfficeAsync(id);

            if (result.IsSuccess)
                return NoContent(); // Success, but no content to return
            else
                return NotFound(result.ErrorMessage);
        }
        //BULK
        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulkPassportOffices([FromBody] List<PassportOfficeDTO> passportOfficesDTO)
        {
            if (passportOfficesDTO == null || !passportOfficesDTO.Any())
                return BadRequest("No data provided.");

            var result = await _passportOfficeService.CreateBulkPassportOfficesAsync(passportOfficesDTO);

            if (result.IsSuccess)
                return Ok(new {message= "Bulk data successfully uploaded." });
            else
                return BadRequest(result.ErrorMessage);
        }
        [HttpDelete("bulkDelete")]
        public async Task<IActionResult> BulkDelete([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
                return BadRequest("No IDs provided.");

            var result = await _passportOfficeService.DeleteSelectedPassportOfficesAsync(ids);

            if (result.IsSuccess)
                return NoContent(); // Success, but no content to return
            else
                return NotFound(result.ErrorMessage);
        }


    }
}
