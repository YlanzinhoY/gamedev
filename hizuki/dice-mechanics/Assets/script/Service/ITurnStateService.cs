using script.Controller;

namespace script.Service
{
    public interface ITurnStateService
    {
        void Play(GameController controller, int sum, MatchState? matchState);
    }
}