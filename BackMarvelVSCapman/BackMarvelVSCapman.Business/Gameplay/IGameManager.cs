
namespace BackMarvelVSCapman.Business.Gameplay
{
    public interface IGameManager
    {
        IReadOnlyCollection<Game> Games { get; }

        void Add(Game game);
        Game CreateGame(string sessionId);
        void Remove(Game game);
    }
}