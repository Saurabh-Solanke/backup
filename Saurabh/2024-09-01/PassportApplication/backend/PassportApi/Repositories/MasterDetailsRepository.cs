using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportApi.Data;
using PassportApi.interfaces;
using PassportApi.Models;

namespace PassportApi.Repositories
{
    public class MasterDetailsRepository : IMasterDetailsRepository
    {
        private readonly PassportDbContext _context;
        public MasterDetailsRepository(PassportDbContext context)
        {
            _context = context;
        }

        public async Task<List<MasterDetailsTable>> GetAllApplicationsWithApplicantName()
        {
            return await _context.MasterDetailsTables.
                        Include(x=>x.ApplicantDetails).ToListAsync();
        }

        public async Task<List<MasterDetailsTable>> GetApplicationStatusByUserId(int id)
        {
          return await _context.MasterDetailsTables.
                Where(x => x.UserId == id)
                .Include(x=>x.ApplicantDetails)
                .ToListAsync();
        }
    }
}
