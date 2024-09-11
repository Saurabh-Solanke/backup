using AutoMapper;
using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.DTOs;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;

namespace Online_Knowledge_Test_Backend_V2.Services.Implementation
{
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository _optionRepository;

        public OptionService(IOptionRepository optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public async Task CreateOptionAsync(Option option)
        {
            await _optionRepository.CreateOptionAsync(option);
        }

        public async Task UpdateOptionAsync(Option option)
        {
            await _optionRepository.UpdateOptionAsync(option);
        }

        public async Task DeleteOptionAsync(int optionId)
        {
            await _optionRepository.DeleteOptionAsync(optionId);
        }
    }
}
