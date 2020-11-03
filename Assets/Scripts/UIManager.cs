using UnityEngine;
using UnityEngine.UI;

public enum Screens
{
    Idle,
    InGame,
    Fail,
    Win,
    Line
}

public class UIManager
{
    private Screens currentScreen;
    private GameObject ui;

    private Slider progress;
    //private GameObject uiPrefab;
    //private GameObject plusOnePrefab;

    public UIManager(GameObject uiPrefab)
    {
        currentScreen = Screens.Idle;
        ui = Object.Instantiate(uiPrefab);
        //plusOnePrefab = ui.transform.GetChild((int)Screens.InGame).GetChild(0).gameObject;
        progress = ui.transform.GetChild((int)Screens.InGame).GetComponentInChildren<Slider>();
    }

    public void ChangeScreen(Screens screen)
    {
        //if (ui == null)
        //    return;

        ui.transform.GetChild((int)currentScreen).gameObject.SetActive(false);
        ui.transform.GetChild((int)screen).gameObject.SetActive(true);
        currentScreen = screen;
    }

    //public void IndicatePlusOne(Camera camera, Vector3 worldPos)
    //{
    //    var pos = camera.WorldToScreenPoint(worldPos);
    //    Debug.Log(pos);
    //    var plusOne = Object.Instantiate(plusOnePrefab,ui.transform.GetChild((int)Screens.InGame).transform);
    //    plusOne.SetActive(true);
    //    plusOne.transform.position = RectTransformUtility.WorldToScreenPoint(camera, worldPos);
    //    Object.Destroy(plusOne, 0.5f);
    //}

    public void UpdateLevelProgress(float value)
    {
        progress.value = value;
    }

}

