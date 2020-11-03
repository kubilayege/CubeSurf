using UnityEngine;
public class InGameState : IGameState
{
    Game game;
    public InGameState(Game g)
    {
        game = g;
        game.uiManager.ChangeScreen(Screens.InGame);
        game.levelManager.player.StartMoving();
    }

    public void CubeDone()
    {
        game.state = new FailState(game);
    }

    public void FinishLine()
    {
        game.state = new LineState(game);
    }

    public void Play()
    {
        return;
    }
}

