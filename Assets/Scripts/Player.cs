using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player
{
    public List<GameObject> currentCubes;
    public GameObject playerObj;
    public GameObject model;
    public int nextHeight;

    GameObject cubePrefab;
    public Movement movement;
    CameraFollow camera;
    float speed;

    public Player(CameraFollow camera, GameObject cubePrefab, GameObject playerModel, int startingCubeCount, Vector3 startingPosition, float speed)
    {
        nextHeight = 0;
        currentCubes= new List<GameObject>();
        this.cubePrefab = cubePrefab;
        this.camera = camera;
        this.speed = speed;
        playerObj = new GameObject("Player");
        playerObj.tag = "Player";

        model = Object.Instantiate(playerModel, new Vector3(0, nextHeight, 0), playerModel.transform.rotation, playerObj.transform);
        model.AddComponent<PlayerCollider>().player = this;


        for (int i = 0; i < startingCubeCount; i++)
        {
            currentCubes.Add(Object.Instantiate(cubePrefab, playerObj.transform.position + new Vector3(0, nextHeight, 0), Quaternion.identity, playerObj.transform));
            nextHeight++;
            model.transform.position = new Vector3(0, nextHeight, 0);

            currentCubes[i].GetComponent<CubeBehaviour>().player = this;
        }

        camera.Follow(playerObj);

    }

    public void StopTurning()
    {
        movement.StopTurning();
    }

    public void StopMoving()
    {
        movement.enabled = false;
        camera.StopMoving();
    }
    public void StartMoving()
    {
        movement = playerObj.AddComponent(typeof(Movement)) as Movement;
        movement.speed = speed;
        camera.StartMoving(speed);
    }

    public void AddNewCube(GameObject collided)
    {
        if (collided != null && !currentCubes.Contains(collided))
        {
            var collidedCube = collided.GetComponent<CubeBehaviour>();
            currentCubes.Add(collided);
            collided.transform.rotation = currentCubes[0].transform.rotation;
            collided.transform.parent = playerObj.transform;
            collided.transform.localPosition =  new Vector3(0, nextHeight, 0);
            nextHeight++;
            model.transform.localPosition = new Vector3(0, nextHeight, 0);
            collidedCube.player = this;

            camera.AdjustCameraPos(1,1);
            movement.StartCoroutine(AnimateCubes(collided));
            movement.StartCoroutine(Jump());
            //GameManager.instance.game.uiManager.IndicatePlusOne(camera.GetComponent<Camera>(), collided.transform.position);
        }
    }

    public void RemoveCube(GameObject cube)
    {
        if (cube != null && currentCubes.Contains(cube))
        {
            cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            cube.transform.parent = null;
            currentCubes.Remove(cube);
            Object.Destroy(cube, 3f);

            camera.AdjustCameraPos(-1,-1);
            if (currentCubes.Count == 0)
                GameManager.instance.game.CubeDone();
            nextHeight--;
        }
    }


    public void FailGame()
    {
        movement.enabled = false;
    }

    public void StartTurning(GameObject curvedRoad)
    {
        movement.Turn(curvedRoad);
    }
    public IEnumerator Jump()
    {
        model.GetComponent<Animator>().SetBool("Jump", true);
        yield return null;
        model.GetComponent<Animator>().SetBool("Jump", false);

    }


    Coroutine cubeAnimCor;
    public IEnumerator AnimateCubes(GameObject cubeToAnim)
    {
        float time = 0.07f;
        float t = 0f;
        var startScale = cubeToAnim.transform.localScale;
        var maxScale = new Vector3(startScale.x * 1.5f, startScale.y, startScale.z * 1.5f);
        while (t < time)
        {
            t += Time.deltaTime;

            cubeToAnim.transform.localScale = Vector3.Lerp(startScale, maxScale, t / time);
            yield return null;
        }
        t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;

            cubeToAnim.transform.localScale = Vector3.Lerp(maxScale,startScale , t / time);
            yield return null;
        }
        cubeToAnim.transform.localScale = startScale;
        
        cubeAnimCor = null;
    }


    public LineRenderer line;
    public int currentLineCount;
    public GameObject lineObj;
    public GameObject currentGrounded;
    public void StartLine(GameObject currentCube)
    {
        if (currentCube == currentGrounded)
            return;
        currentGrounded = currentCube;
        if (lineObj != null)
            Object.Destroy(lineObj,1);
        lineObj = new GameObject();
        lineObj.transform.position = playerObj.transform.position;
        lineObj.transform.forward = Vector3.down;
        lineObj.transform.parent = playerObj.transform;
        line = lineObj.AddComponent<LineRenderer>();
        line.SetPositions(new Vector3[0]);
        line.enabled = true;
        line.alignment = LineAlignment.TransformZ;
        line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        line.material = GameManager.instance.cubeMat;
        line.SetPosition((0) % 1000, playerObj.transform.position);
        line.SetPosition((1) % 1000, playerObj.transform.position);
    }
    public void DrawLine(GameObject cube)
    {
        var pos = new Vector3(cube.transform.position.x, -0.48f, cube.transform.position.z);
        line.positionCount++;
        line.SetPosition((line.positionCount - 1) % 1000, pos);
        currentLineCount++;
    }
}
