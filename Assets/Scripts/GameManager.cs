using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelManager levelManager;
    public UIManager uiManager;
    public GameObject uiPrefab;

    public Game game;
    public Material cubeMat;
    void Start()
    {
        instance = this;
        uiManager = new UIManager(uiPrefab);
        levelManager = new LevelManager();
        game = new Game(uiManager, levelManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);


        if (Input.GetMouseButtonDown(0))
        {
            game.Play();
        }

    }
}
