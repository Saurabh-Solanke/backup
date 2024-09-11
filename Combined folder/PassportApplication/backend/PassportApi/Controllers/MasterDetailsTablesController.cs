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
using PassportApi.Dtos.ApplicationStatusDto;
using PassportApi.interfaces;
using PassportApi.Models;
using PassportApi.Models.Enums;

namespace PassportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDetailsTablesController : ControllerBase
    {
        private readonly IRootRepository<MasterDetailsTable> _rootRepo;
        private readonly IMapper _mapper;
        private readonly IMasterDetailsRepository _masterRepo;

        public MasterDetailsTablesController(IRootRepository<MasterDetailsTable> rootRepo, IMapper mapper, IMasterDetailsRepository masterRepo)
        {
            _masterRepo = masterRepo;
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/MasterDetailsTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterDetailsTableRespDTO>>> GetMasterDetailsTables()
        {
            try
            {
                var masterDetailsTables = await _masterRepo.GetAllApplicationsWithApplicantName();
                List<MasterDetailsTableRespDTO> masterDto = new List<MasterDetailsTableRespDTO>();
                foreach (var master in masterDetailsTables)
                {
                    MasterDetailsTableRespDTO dto = new MasterDetailsTableRespDTO
                    {
                        ApplicantName = master.ApplicantDetails.ApplicantFirstName + " "+ master.ApplicantDetails.ApplicantLastName,
                        ApplicationNo=master.ApplicationNo,
                        ApplicationStatus=master.ApplicationStatus,
                        PassportType = master.PassportType,
                        CreatedOn = master.CreatedOn,
                        UserId = master.UserId

                    };
                    masterDto.Add(dto);
                }
                

                return Ok(masterDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the master details tables.");
            }
        }

        

        // GET: api/MasterDetailsTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterDetailsTableDTO>> GetMasterDetailsTable(int id)
        {
            try
            {
                var masterDetailsTable = await _rootRepo.GetByIdAsync(id);
                if (masterDetailsTable == null)
                {
                    return NotFound($"Master details table with ID {id} not found.");
                }

                var masterDetailsTableDto = _mapper.Map<MasterDetailsTableDTO>(masterDetailsTable);
                return Ok(masterDetailsTableDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the master details table.");
            }
        }

        // PUT: api/MasterDetailsTables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMasterDetailsTable(int id, ApplicationUpdateDTO applicationUpdateDTO)
        {
            if (id != applicationUpdateDTO.ApplicationNo)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var masterDetailsTable = await _rootRepo.GetByIdAsync(applicationUpdateDTO.ApplicationNo);
                masterDetailsTable.ApplicationStatus = applicationUpdateDTO.ApplicationStatus;
                await _rootRepo.UpdateAsync(id, masterDetailsTable);
                return Ok(masterDetailsTable.UserId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Master details table with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the master details table.");
            }
        }

        // POST: api/MasterDetailsTables
        [HttpPost]
        public async Task<ActionResult<MasterDetailsTableDTO>> PostMasterDetailsTable(MasterDetailsTableDTO masterDetailsTableDto)
        {
            try
            {
                var masterDetailsTable = _mapper.Map<MasterDetailsTable>(masterDetailsTableDto);
                masterDetailsTable.PassportNo = DateTime.Now.ToString("yyyyMMddHHmm"); // Assuming this is intended logic
                Console.WriteLine(masterDetailsTable);
                var newMasterDetailsTable = await _rootRepo.AddAsync(masterDetailsTable);
                var newMasterDetailsTableDto = _mapper.Map<MasterDetailsTableDTO>(newMasterDetailsTable);

                return CreatedAtAction(nameof(GetMasterDetailsTable), new { id = newMasterDetailsTableDto.ApplicationNo }, newMasterDetailsTableDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the master details table.");
            }
        }

        // DELETE: api/MasterDetailsTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterDetailsTable(int id)
        {
            try
            {
                var masterDetailsTable = await _rootRepo.GetByIdAsync(id);
                if (masterDetailsTable == null)
                {
                    return NotFound($"Master details table with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the master details table.");
            }
        }

        [HttpGet("application-status/{id}")]
        public async Task<IActionResult> GetApplicationStatusByUserId(int id)
        {
            try
            {
                List<MasterDetailsTable> masterDetail = await _masterRepo.GetApplicationStatusByUserId(id);
                if (masterDetail == null)
                {
                    return NotFound("User Id Not Found");
                }
                List<ApplicationStatusResponseDTO> applicationStatusResponseDTOs = new List<ApplicationStatusResponseDTO>();
                foreach (var master in masterDetail)
                {
                    ApplicationStatusResponseDTO applicationStatusResponseDTO = new ApplicationStatusResponseDTO
                    {
                        ApplicationId=master.ApplicationNo,
                        ApplicationStatus = master.ApplicationStatus.ToString(),
                        ApplicationType = master.PassportType.ToString(),
                        PassportNo = master.PassportNo,
                        PaymentStatus = master.PaymentStatus.ToString(),
                        ApplicantName = master.ApplicantDetails.ApplicantFirstName + " "+ master.ApplicantDetails.ApplicantLastName,

                    };
                    applicationStatusResponseDTOs.Add(applicationStatusResponseDTO);
                }

                return Ok(applicationStatusResponseDTOs);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}
