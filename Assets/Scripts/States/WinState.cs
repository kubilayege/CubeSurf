using UnityEngine;
public class WinState : IGameState
{
    Game game;
    public WinState(Game g)
    {
        game = g;
        game.uiManager.ChangeScreen(Screens.Win);
        game.levelManager.player.StopMoving();
    }

    public void CubeDone()
    {
        return;
    }

    public void FinishLine()
    {
        return;
    }

    public void Play()
    {
        game.state = new IdleState(game, InitType.Pass);
    }
}

