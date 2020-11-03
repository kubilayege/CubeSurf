using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Level", menuName = "New Level" , order = 2)]
public class Level : ScriptableObject
{
    public GameObject cubeModel;
    public GameObject playerModel;
    
    [Range(1,10)]
    public float speed;
    public int startingCubeCount;

    public GameObject levelPrefab;


}
