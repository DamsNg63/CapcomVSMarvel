using BackMarvelVSCapman.DAL.Model;
using BackMarvelVSCapman.DAL.Repository;
using BackMarvelVSCapman.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.Business.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterService(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public void Create(CreateChraraterDto createChraraterDto)
        {
            _characterRepository.Add(new Character
            {
                Name = createChraraterDto.Name,
                Image = createChraraterDto.Image,
                TeamId = createChraraterDto.TeamId
            });
        }
    }
}
