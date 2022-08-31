using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public int Time = 0;
    private SimpleTimer timer;
    public GameObject ProgressBar;
    public BuildingManager buildingManager;
    private Sprite[] sprites;

    public void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites\\UI\\ProgressBar");
    }

    public void Update()
    {
        CalculateTime();
    }
    private void CalculateTime()
    {
        if (timer == null)
        {
            timer = new SimpleTimer(1000);
        }
        else if (timer.IsRunning) { }
        else
        {
            Time++;
            TimeSpan interval = TimeSpan.FromSeconds(Time);
            string timeInterval = interval.ToString();
            ProgressBar.GetComponentInChildren<TextMeshProUGUI>().text = timeInterval;
            ProgressBar.GetComponentInChildren<Image>().sprite = sprites[Time / 60];

            timer = new SimpleTimer(500);
        }
    }
    public void SetBuilding(string building)
    {
        buildingManager.SetCurrentBuilding(building);
    }
}
