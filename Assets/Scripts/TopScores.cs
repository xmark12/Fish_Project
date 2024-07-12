using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopScores : MonoBehaviour
{
    public TMP_Text easyTime;
    public TMP_Text easyDistance;

    public TMP_Text mediumTime;
    public TMP_Text mediumDistance;

    public TMP_Text hardTime;
    public TMP_Text hardDistance;

    // Start is called before the first frame update
    void Start()
    {
        float easyMinutes = Mathf.FloorToInt((PlayerPrefs.GetFloat("EasyTime", 0) + 1) / 60);
        float easySeconds = Mathf.FloorToInt((PlayerPrefs.GetFloat("EasyTime", 0) + 1) % 60);
        easyTime.text = "Time: " + string.Format("{0:00} : {1:00}", easyMinutes, easySeconds);

        float mediumMinutes = Mathf.FloorToInt((PlayerPrefs.GetFloat("MediumTime", 0) + 1) / 60);
        float mediumSeconds = Mathf.FloorToInt((PlayerPrefs.GetFloat("MediumTime", 0) + 1) % 60);
        mediumTime.text = "Time: " + string.Format("{0:00} : {1:00}", mediumMinutes, mediumSeconds);

        float hardMinutes = Mathf.FloorToInt((PlayerPrefs.GetFloat("HardTime", 0) + 1) / 60);
        float hardSeconds = Mathf.FloorToInt((PlayerPrefs.GetFloat("HardTime", 0) + 1) % 60);
        hardTime.text = "Time: " + string.Format("{0:00} : {1:00}", hardMinutes, hardSeconds);

        easyDistance.text = "Distance: " + PlayerPrefs.GetFloat("EasyDist", 0).ToString("F2") + " Feesh Per-Cool";
        mediumDistance.text = "Distance: " + PlayerPrefs.GetFloat("MediumDist", 0).ToString("F2") + " Feesh Per-Cool";
        hardDistance.text = "Distance: " + PlayerPrefs.GetFloat("HardDist", 0).ToString("F2") + " Feesh Per-Cool";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
