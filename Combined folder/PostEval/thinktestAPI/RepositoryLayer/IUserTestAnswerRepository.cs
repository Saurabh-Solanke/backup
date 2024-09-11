using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Knowledge__Test_Backend.DTOs;
using Online_Knowledge__Test_Backend.Models;

namespace Online_Knowledge__Test_Backend.RepositoryLayer
{
    public interface IUserTestAnswerRepository
    {
        Task<IEnumerable<UserTestAnswer>> GetAnswersByTestIdAsync(int testId);
        Task<IEnumerable<UserTestAnswer>> GetAnswersByUserIdAndTestIdAsync(int userId, int testId);

        // Add a single UserTestAnswer to the database
        Task AddAsync(UserTestAnswer userTestAnswer);
    }
}
