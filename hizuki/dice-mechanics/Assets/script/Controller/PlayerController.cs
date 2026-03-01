using script.Service;

namespace script.Controller
{
    public class PlayerController : ITurnStateService
    {
        public void Play(GameController controller, int sum, MatchState? matchState)
        {
            controller.ResolveRoll(sum, matchState, true);

            if (controller.AttemptsEnded())
                controller.ChangeTurn(new CpuController());
        }
    }
}