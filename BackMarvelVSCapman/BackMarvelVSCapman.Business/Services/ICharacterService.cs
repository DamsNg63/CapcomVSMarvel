using BackMarvelVSCapman.DTO;

namespace BackMarvelVSCapman.Business.Services
{
    public interface ICharacterService
    {
        Task<bool> Create(CreateChraraterDto createChraraterDto);
    }
}