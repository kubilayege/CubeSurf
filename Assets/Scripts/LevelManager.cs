using UnityEngine;

public enum InitType
{
    Restart,
    Pass
}

public class LevelManager
{
    public int currentLevelIndex;
    
    [SerializeField]
    private Level[] levels;
    private Level currentLevelData;
    public GameObject currentLevel;
   
    
    public Player player;
    public CameraFollow camera;
    

    public LevelManager()
    {
        levels = Resources.LoadAll<Level>("Levels/");
    }

    // Update is called once per frame
    public void InitLevel(InitType type)
    {
        if (currentLevel != null)
        {
            Object.Destroy(currentLevel);
            Object.Destroy(player.playerObj);
            player = null;
        }


        currentLevelIndex += (int)type;
        currentLevelData = levels[currentLevelIndex % levels.Length];
        currentLevel = Object.Instantiate(currentLevelData.levelPrefab);


        camera = Object.FindObjectOfType<CameraFollow>();
        
        player = new Player(camera, currentLevelData.cubeModel, currentLevelData.playerModel,
                            currentLevelData.startingCubeCount, Vector3.zero, currentLevelData.speed);
       
    }
}
