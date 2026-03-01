using script.Service;

namespace script.Controller
{
    public class CpuController : ITurnStateService
    {
        public void Play(GameController controller, int sum, MatchState? matchState)
        {
            controller.ResolveRoll(sum,matchState, false);

            if (controller.AttemptsEnded())
            {
                controller.ChangeTurn(new PlayerController());
            }
            else
            {
                controller.RollCpu();
            }
                
        }
    }
}