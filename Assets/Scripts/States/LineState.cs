using System;

class LineState : IGameState
{
    Game game;
    public LineState(Game g)
    {
        game = g;
        game.uiManager.ChangeScreen(Screens.Line);
    }

    public void CubeDone()
    {
        game.state = new WinState(game);
    }

    public void FinishLine()
    {
        return;
    }

    public void Play()
    {
        return;
    }
}
