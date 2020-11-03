using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Range(1, 20)]
    public float speed;
    public float horSpeed;
    private Vector3 forward;
    private Vector3 right;
    private Vector3 lastTocuhPlace;
    private Vector3 currentTocuhPlace;
    private WayPointControl wpController;
    private float levelLenght;
    private float playerTravelLenght;
    public void Start()
    {
        forward = transform.forward;
        right = transform.right;
        horSpeed = speed;
        lastTocuhPlace = Input.mousePosition;
        wpController = GameManager.instance.levelManager.currentLevel.GetComponentInChildren<WayPointControl>();
        levelLenght = wpController.GetLenght();
    }
    // Update is called once per frame
    void Update()
    {
        var forwardTravel = forward * speed * Time.deltaTime;
        transform.Translate(forwardTravel);
        playerTravelLenght += (forwardTravel).magnitude;
        GameManager.instance.game.uiManager.UpdateLevelProgress(playerTravelLenght / levelLenght);


        if (Input.GetMouseButtonDown(0))
        {
            lastTocuhPlace = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentTocuhPlace = Input.mousePosition;
            var dir = currentTocuhPlace - lastTocuhPlace;



            if (dir.magnitude <= 3)
                lastTocuhPlace = currentTocuhPlace;
            else
            {
                horSpeed = Mathf.Clamp(dir.magnitude, 0, 21);
                var nextPlace = transform.position - right * Mathf.Sign(dir.x) * horSpeed *  Time.deltaTime;

                transform.Translate(CheckBorder(nextPlace));
                lastTocuhPlace = currentTocuhPlace;
            }
            

        }


        //if (Input.GetAxisRaw("Horizontal") != 0)
        //{
        //    var nextPlace = transform.position - right * Mathf.CeilToInt(Input.GetAxisRaw("Horizontal")) * horSpeed * Time.deltaTime;

        //    transform.Translate(CheckBorder(nextPlace));
        //}

        if (Input.GetKeyDown(KeyCode.W))
            speed *= 1.25f;
        if (Input.GetKeyDown(KeyCode.S))
            speed *= 0.75f;

    }

    private Vector3 CheckBorder(Vector3 nextPlace)
    {
        var dir = Mathf.Sign((transform.position - nextPlace).x);
        Debug.Log(dir);
        var origin = transform.position + dir*transform.right*0.62f + transform.up ;
        if (Physics.Raycast(origin, Vector3.down ,out RaycastHit hit))
        {
            if(hit.collider != null)
            {
                return transform.position-nextPlace;
            }
        }
        return Vector3.zero;
    }

    Coroutine turningCor;
    bool turn;
    public void Turn(GameObject curvedRoad)
    {
        currentRoad = curvedRoad;
        turn = true;
        Debug.Log(curvedRoad.name);
        var pivot = curvedRoad.transform.GetChild(0).transform;
        turningCor = StartCoroutine(Turning(pivot.position,pivot.forward));
    }

    GameObject currentRoad;

    public void StopTurning()
    {
        turn = false;
        StopCoroutine(turningCor);
        turningCor = null;
        transform.forward = -currentRoad.transform.right/*Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y < 20f ? 0f : -90f, transform.localEulerAngles.z)*/;
        Camera.main.transform.parent.forward = transform.forward;
        Camera.main.GetComponent<CameraFollow>().RecalculateOffset(); 
    }
    public IEnumerator Turning(Vector3 pivot, Vector3 pivotUP)
    {
        pivot = new Vector3(pivot.x, transform.position.y, pivot.z);
        Debug.Log(transform.localEulerAngles.y);
        var dir = (pivot - transform.position);
        var forward = Vector3.Cross(-dir, pivotUP);
        //Camera.main.GetComponent<CameraFollow>().speed*=4f;
        while (turn)
        {
            dir = (pivot - transform.position);
            forward = Vector3.Cross(-dir, pivotUP);
            transform.forward = forward;
            Camera.main.transform.parent.forward = forward;/*Vector3.Lerp(Camera.main.transform.parent.forward, forward, Time.deltaTime * 5f);*/
            Camera.main.GetComponent<CameraFollow>().RecalculateOffset(); 
            yield return null;
        }
        this.forward = forward;
        this.right = transform.right;
        Debug.Log(transform.localEulerAngles.y);
        Debug.Log("EndTurn");
    }

}
