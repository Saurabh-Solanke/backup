using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PassPortal_API.Data;
using PassPortal_API.DTOs;
using PassPortal_API.Models;

namespace PassPortal_API.Repositories
{
    public class PassportOfficeRepository
    {
        private readonly ApplicationDbContext _context;

        public PassportOfficeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PassportOffice> GetPassportOfficeByIdAsync(int id)
        {
            return await _context.PassportOffices.FindAsync(id);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> BulkInsertPassportOfficesAsync(IEnumerable<PassportOffice> passportOffices)
        {
            try
            {
                await _context.PassportOffices.AddRangeAsync(passportOffices);
                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch
            {
                return (false, "Error inserting passport offices.");
            }
        }

        public async Task<IEnumerable<PassportOffice>> GetAllPassportOfficesAsync()
        {
            return await _context.PassportOffices.ToListAsync();
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdatePassportOfficeAsync(PassportOffice passportOffice)
        {
            try
            {
                _context.PassportOffices.Update(passportOffice);
                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch
            {
                return (false, "Error updating passport office.");
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AddPassportOfficeAsync(PassportOffice passportOffice)
        {
            try
            {
                _context.PassportOffices.Add(passportOffice);
                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                // Log the exception message and return an error
                return (false, ex.Message);
            }
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> DeletePassportOfficeAsync(PassportOffice passportOffice)
        {
            try
            {
                _context.PassportOffices.Remove(passportOffice);
                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                // Log the exception message and return an error
                return (false, ex.Message);
            }
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> CreateBulkPassportOfficesAsync(IEnumerable<PassportOffice> passportOffices)
        {
            try
            {
                // Start a transaction to ensure all or nothing is saved
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    await _context.PassportOffices.AddRangeAsync(passportOffices);
                    await _context.SaveChangesAsync();

                    // Commit the transaction
                    await transaction.CommitAsync();
                }

                return (true, null);
            }
            catch (Exception ex)
            {
                // Log the exception message and return an error
                return (false, ex.Message);
            }
        }
        public async Task<IEnumerable<PassportOffice>> GetPassportOfficesByIdsAsync(List<int> ids)
        {
            return await _context.PassportOffices
                .Where(po => ids.Contains(po.Id))
                .ToListAsync();
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteBulkPassportOfficesAsync(IEnumerable<PassportOffice> passportOffices)
        {
            try
            {
                _context.PassportOffices.RemoveRange(passportOffices);
                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                // Log the exception message and return an error
                return (false, ex.Message);
            }
        }
  

       



    }
}
