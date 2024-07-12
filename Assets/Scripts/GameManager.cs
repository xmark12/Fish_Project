using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject spawnObjectMines;
    public GameObject spawnObjectHooks;
    public GameObject spawnObjectNets;
    public GameObject player;

    public GameObject[] spawnPointsMines;
    public GameObject[] spawnPointsHooks;
    public GameObject[] spawnPointsNets;

    public float timer;
    public float timeBetweenSpawns;

    public float speedDifficultyScale;
    public float speedMultiplier;
    public float maxSpeedMultiplier;

    public float speedParallax;
    public float parallaxOffsetVal;

    [Header("Time & Distance")]
    public TMP_Text distText;
    private float distance;

    public float time = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;

    [Header("Parallax")]
    public parallax p;

    [Header("Shark")]
    public GameObject shark;

    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().StopMusic();

        if (PlayerPrefs.GetInt("EasyBool") != 0 && PlayerPrefs.GetInt("MediumBool") == 0 && PlayerPrefs.GetInt("HardBool") == 0)
        {
            speedMultiplier = 0;
            maxSpeedMultiplier = 10;
            parallaxOffsetVal = 10;
        }
        if (PlayerPrefs.GetInt("MediumBool") != 0 && PlayerPrefs.GetInt("EasyBool") == 0 && PlayerPrefs.GetInt("HardBool") == 0)
        {
            speedMultiplier = 5;
            maxSpeedMultiplier = 15;
            parallaxOffsetVal = 15;
        }
        if (PlayerPrefs.GetInt("HardBool") != 0 && PlayerPrefs.GetInt("EasyBool") == 0 && PlayerPrefs.GetInt("MediumBool") == 0)
        {
            speedMultiplier = 10;
            maxSpeedMultiplier = 20;
            parallaxOffsetVal = 20;
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < p.backgrounds.Length; i++)
        {
            Renderer rend = p.backgrounds[i].GetComponent<Renderer>();

            p.offset = (speedParallax) * (p.scrollSpeed[i] + (parallaxOffsetVal * 0.035f));

            rend.material.SetTextureOffset("_MainTex", new Vector2(p.offset, 0));
        }
    }

    void DisplayTime (float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = "Time: " + string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    void CheckEasyTime()
    {
        if (time > PlayerPrefs.GetFloat("EasyTime", 0))
        {
            PlayerPrefs.SetFloat("EasyTime", time);
        }
    }
    void CheckMediumTime()
    {
        if (time > PlayerPrefs.GetFloat("MediumTime", 0))
        {
            PlayerPrefs.SetFloat("MediumTime", time);
        }
    }
    void CheckHardTime()
    {
        if (time > PlayerPrefs.GetFloat("HardTime", 0))
        {
            PlayerPrefs.SetFloat("HardTime", time);
        }
    }

    void CheckEasyDist()
    {
        if (distance > PlayerPrefs.GetFloat("EasyDist", 0))
        {
            PlayerPrefs.SetFloat("EasyDist", distance);
        }
    }
    void CheckMediumDist()
    {
        if (distance > PlayerPrefs.GetFloat("MediumDist", 0))
        {
            PlayerPrefs.SetFloat("MediumDist", distance);
        }
    }
    void CheckHardDist()
    {
        if (distance > PlayerPrefs.GetFloat("HardDist", 0))
        {
            PlayerPrefs.SetFloat("HardDist", distance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().StopMusic();

        if (PlayerPrefs.GetInt("Shark") != 0)
        {
            shark.SetActive(true);
        }
        else
        {
            shark.SetActive(false);
        }

        if (timeIsRunning)
        {
            if (time >= 0)
            {
                time += Time.deltaTime;
                DisplayTime(time);

                if (PlayerPrefs.GetInt("EasyBool") != 0 && PlayerPrefs.GetInt("MediumBool") == 0 && PlayerPrefs.GetInt("HardBool") == 0)
                {
                    CheckEasyTime();
                }
                if (PlayerPrefs.GetInt("MediumBool") != 0 && PlayerPrefs.GetInt("EasyBool") == 0 && PlayerPrefs.GetInt("HardBool") == 0)
                {
                    CheckMediumTime();
                }
                if (PlayerPrefs.GetInt("HardBool") != 0 && PlayerPrefs.GetInt("EasyBool") == 0 && PlayerPrefs.GetInt("MediumBool") == 0)
                {
                    CheckHardTime();
                }
            }
        }
        
        distText.text = "Distance: " + distance.ToString("F2") + " Feesh Per-Cool";
        distance += Time.deltaTime * speedMultiplier;

        if (PlayerPrefs.GetInt("EasyBool") != 0 && PlayerPrefs.GetInt("MediumBool") == 0 && PlayerPrefs.GetInt("HardBool") == 0)
        {
            CheckEasyDist();
        }
        if (PlayerPrefs.GetInt("MediumBool") != 0 && PlayerPrefs.GetInt("EasyBool") == 0 && PlayerPrefs.GetInt("HardBool") == 0)
        {
            CheckMediumDist();
        }
        if (PlayerPrefs.GetInt("HardBool") != 0 && PlayerPrefs.GetInt("EasyBool") == 0 && PlayerPrefs.GetInt("MediumBool") == 0)
        {
            CheckHardDist();
        }

        if (speedMultiplier <= maxSpeedMultiplier)
            speedMultiplier += Time.deltaTime * speedDifficultyScale;
        else
            speedMultiplier = maxSpeedMultiplier;

        speedParallax += Time.deltaTime * speedDifficultyScale;

        if (PlayerPrefs.GetInt("EasyBool") == 0)
        {
            if (speedMultiplier < 5)
            {
                timeBetweenSpawns = 4;
            }
            if (speedMultiplier >= 5 && speedMultiplier < 10)
            {
                timeBetweenSpawns = 3;
            }
            if (speedMultiplier >= 10 && speedMultiplier < 20)
            {
                timeBetweenSpawns = 2;
            }
            if (speedMultiplier >= 20)
            {
                timeBetweenSpawns = 1;
            }
        }
        else
        {
            timeBetweenSpawns = 4;
        }

        timer += Time.deltaTime;

        if (timer > timeBetweenSpawns)
        {
            timer = 0;
            int randNum = Random.Range(0, 4);
            //Instantiate(spawnObjectMines, spawnPointsMines[randNum].transform.position, Quaternion.identity);

            spawnObjectMines = ObjectPool.instance.GetPooledObjectMine();
            if (spawnObjectMines != null)
            {
                spawnObjectMines.transform.position = spawnPointsMines[randNum].transform.position;
                spawnObjectMines.SetActive(true);
            }

            spawnObjectHooks = ObjectPool.instance.GetPooledObjectHook();
            if (spawnObjectHooks != null)
            {
                spawnObjectHooks.transform.position = spawnPointsHooks[randNum].transform.position;
                spawnObjectHooks.SetActive(true);
            }

            spawnObjectNets = ObjectPool.instance.GetPooledObjectNet();
            if (spawnObjectNets != null)
            {
                spawnObjectNets.transform.position = spawnPointsNets[randNum].transform.position;
                spawnObjectNets.SetActive(true);
            }
        }
    }
}
