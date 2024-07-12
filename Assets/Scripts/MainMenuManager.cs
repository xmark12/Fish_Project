using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public bool easyTrigger = false;
    public bool mediumTrigger = false;
    public bool hardTrigger = false;
    public bool sharkTrigger = false;

    public GameObject sharkToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();

        sharkTrigger = sharkToggle.GetComponent<Toggle>().isOn;
        PlayerPrefs.SetInt("Shark", (sharkTrigger ? 1 : 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchEasy()
    {
        Time.timeScale = 1f;

        easyTrigger = true;
        mediumTrigger = false;
        hardTrigger = false;
        PlayerPrefs.SetInt("EasyBool", (easyTrigger ? 1 : 0));
        PlayerPrefs.SetInt("MediumBool", (mediumTrigger ? 1 : 0));
        PlayerPrefs.SetInt("HardBool", (hardTrigger ? 1 : 0));

        SceneManager.LoadScene(1);
    }
    public void SwitchMedium()
    {
        Time.timeScale = 1f;

        easyTrigger = false;
        mediumTrigger = true;
        hardTrigger = false;
        PlayerPrefs.SetInt("EasyBool", (easyTrigger ? 1 : 0));
        PlayerPrefs.SetInt("MediumBool", (mediumTrigger ? 1 : 0));
        PlayerPrefs.SetInt("HardBool", (hardTrigger ? 1 : 0));

        SceneManager.LoadScene(1);
    }
    public void SwitchHard()
    {
        Time.timeScale = 1f;

        easyTrigger = false;
        mediumTrigger = false;
        hardTrigger = true;
        PlayerPrefs.SetInt("EasyBool", (easyTrigger ? 1 : 0));
        PlayerPrefs.SetInt("MediumBool", (mediumTrigger ? 1 : 0));
        PlayerPrefs.SetInt("HardBool", (hardTrigger ? 1 : 0));

        SceneManager.LoadScene(1);
    }

    public void SwitchMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void SwitchControls()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }

    public void SwitchCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void SwitchDifficulty()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }

    public void TriggerShark(bool toggle)
    {
        sharkTrigger = toggle;
        PlayerPrefs.SetInt("Shark", (sharkTrigger ? 1 : 0));
    }
}
