using BackMarvelVSCapman.DTO;

namespace BackMarvelVSCapman.Business.Services
{
    public interface ICharacterService
    {
        bool Create(CreateChraraterDto createChraraterDto);
    }
}