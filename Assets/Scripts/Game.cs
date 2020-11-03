using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public IGameState state;
    public UIManager uiManager;
    public LevelManager levelManager;
    public Player player;

    public CameraFollow camera;
    public Game(UIManager uiManager, LevelManager levelManager)
    {
        this.uiManager = uiManager;
        this.levelManager = levelManager;
        state = new IdleState(this, InitType.Restart);
    }
    
    public void CubeDone()
    {
        state.CubeDone();
        Debug.Log(state);
    }

    public void FinishLine()
    {
        state.FinishLine();
        Debug.Log(state);
    }

    public void Play()
    {
        state.Play();
        Debug.Log(state);
    }
}
