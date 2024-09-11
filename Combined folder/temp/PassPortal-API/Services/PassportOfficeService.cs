using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassPortal_API.Repositories;
using PassPortal_API.DTOs;
using PassPortal_API.Models;
using Microsoft.AspNetCore.Http;

namespace PassPortal_API.Services
{
    public class PassportOfficeService
    {
        private readonly PassportOfficeRepository _passportOfficeRepository;
       

        public PassportOfficeService(PassportOfficeRepository passportOfficeRepository)
        {
            _passportOfficeRepository = passportOfficeRepository;
           /* _csvHelperService = csvHelperService;*/
        }

        /*public async Task<(bool IsSuccess, string ErrorMessage)> ProcessCsvFileAsync(IFormFile file)
        {
            var passportOfficeDTOs = await _csvHelperService.ParseCsvFileAsync(file);

            if (passportOfficeDTOs == null)
                return (false, "Error parsing CSV file.");

            // Convert DTOs to entities
            var passportOffices = passportOfficeDTOs.Select(dto => new PassportOffice
            {
                OfficeName = dto.OfficeName,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                ContactNumber = dto.ContactNumber,
                Email = dto.Email
            }).ToList();

            var result = await _passportOfficeRepository.BulkInsertPassportOfficesAsync(passportOffices);

            return result.IsSuccess ? (true, null) : (false, "Error inserting data into the database.");
        }
*/
        public async Task<IEnumerable<PassportOffice>> GetAllPassportOfficesAsync()
        {
            var passportOffices = await _passportOfficeRepository.GetAllPassportOfficesAsync();

            // Convert entities to DTOs
            return passportOffices.Select(po => new PassportOffice
            {
                Id=po.Id,
                OfficeName = po.OfficeName,
                City = po.City,
                State = po.State,
                Country = po.Country,
                ContactNumber = po.ContactNumber,
                Email = po.Email
            }).ToList();
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdatePassportOfficeAsync(int id, PassportOfficeDTO passportOfficeDTO)
        {
            var passportOffice = await _passportOfficeRepository.GetPassportOfficeByIdAsync(id);
            if (passportOffice == null)
                return (false, "Passport office not found.");

            // Update the passport office properties with the new data
            passportOffice.OfficeName = passportOfficeDTO.OfficeName;
            passportOffice.City = passportOfficeDTO.City;
            passportOffice.State = passportOfficeDTO.State;
            passportOffice.Country = passportOfficeDTO.Country;
            passportOffice.ContactNumber = passportOfficeDTO.ContactNumber;
            passportOffice.Email = passportOfficeDTO.Email;

            var result = await _passportOfficeRepository.UpdatePassportOfficeAsync(passportOffice);
            return result.IsSuccess ? (true, null) : (false, "Error updating passport office.");
        }

        public async Task<(bool IsSuccess, string ErrorMessage, int CreatedId)> CreatePassportOfficeAsync(PassportOfficeDTO passportOfficeDTO)
        {
            // Convert DTO to entity
            var passportOffice = new PassportOffice
            {
                OfficeName = passportOfficeDTO.OfficeName,
                City = passportOfficeDTO.City,
                State = passportOfficeDTO.State,
                Country = passportOfficeDTO.Country,
                ContactNumber = passportOfficeDTO.ContactNumber,
                Email = passportOfficeDTO.Email
            };

            // Save to repository
            var result = await _passportOfficeRepository.AddPassportOfficeAsync(passportOffice);
            return result.IsSuccess ? (true, null, passportOffice.Id) : (false, result.ErrorMessage, 0);
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> DeletePassportOfficeAsync(int id)
        {
            var passportOffice = await _passportOfficeRepository.GetPassportOfficeByIdAsync(id);
            if (passportOffice == null)
                return (false, "Passport office not found.");

            var result = await _passportOfficeRepository.DeletePassportOfficeAsync(passportOffice);
            return result.IsSuccess ? (true, null) : (false, "Error deleting passport office.");
        }
        public async Task<Result> CreateBulkPassportOfficesAsync(List<PassportOfficeDTO> passportOfficesDTO)
        {
            var result = new Result();

            try
            {
                foreach (var passportOfficeDTO in passportOfficesDTO)
                {
                    // Ensure valid data and perform the creation
                    var createResult = await CreatePassportOfficeAsync(passportOfficeDTO);
                    if (!createResult.IsSuccess)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessage += createResult.ErrorMessage + " ";
                    }
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteSelectedPassportOfficesAsync(List<int> ids)
        {
            var passportOffices = await _passportOfficeRepository.GetPassportOfficesByIdsAsync(ids);

            if (passportOffices == null || !passportOffices.Any())
                return (false, "No passport offices found for the given IDs.");

            var result = await _passportOfficeRepository.DeleteBulkPassportOfficesAsync(passportOffices);
            return result.IsSuccess ? (true, null) : (false, "Error deleting passport offices.");
        }
    


    }
}
