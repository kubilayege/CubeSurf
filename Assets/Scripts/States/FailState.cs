internal class FailState : IGameState
{
    private Game game;

    public FailState(Game game)
    {
        this.game = game;
        game.uiManager.ChangeScreen(Screens.Fail);
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
        game.state = new IdleState(game, InitType.Restart);
    }
}