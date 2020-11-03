using UnityEngine;

public class IdleState : IGameState
{
    Game game;
    public IdleState(Game game, InitType type)
    {
        this.game = game;
        game.uiManager.ChangeScreen(Screens.Idle);
        game.levelManager.InitLevel(type);
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
        game.state = new InGameState(game);
        return;
    }
}