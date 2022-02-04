using BackMarvelVSCapman.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.Business.Gameplay
{
    public class Player
    {
        public Guid PlayerId { get; }
        public string SessionId { get; set; } = string.Empty;

        public (Character C1, Character C2, Character C3)? Characters { get; set; }

        public Player((Character, Character, Character)? characters = null)
        {
            PlayerId = Guid.NewGuid();
            Characters = characters;
        }
    }
}
