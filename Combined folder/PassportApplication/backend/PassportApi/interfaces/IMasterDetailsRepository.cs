using Microsoft.AspNetCore.Mvc;
using PassportApi.Models;

namespace PassportApi.interfaces
{
    public interface IMasterDetailsRepository
    {
        Task<List<MasterDetailsTable>> GetApplicationStatusByUserId(int id);

        Task<List<MasterDetailsTable>> GetAllApplicationsWithApplicantName();

    }
}
