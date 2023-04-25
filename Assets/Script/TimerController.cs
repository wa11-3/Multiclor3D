using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{

    public float timerGame;
    public bool[] helper = { true, true, true, true, true};

    public static TimerController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        timerGame = 660.0f;
    }

    private void Update()
    {
        timerGame -= Time.deltaTime;
        DialogHelper();
    }

    public string TimerManager()
    {
        float minutes = Mathf.FloorToInt(timerGame / 60);
        float seconds = Mathf.FloorToInt(timerGame % 60);
        return string.Format("{00:00}:{1:00}", minutes, seconds);
    }

    void DialogHelper()
    {
        if ((timerGame <= 480 && helper[0]))
        {
            helper[0] = false;
            if (SceneManager.GetActiveScene().name == "Laboratory")
            {
                bool haveHammer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().viewGlass;
                if (haveHammer)
                {
                    DialogControler.Instance.TimeDialog(1);
                }
                else
                {
                    DialogControler.Instance.TimeDialog(0);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Castle")
            {
                DialogControler.Instance.TimeDialog(0);
            }
            else if (SceneManager.GetActiveScene().name == "Departament")
            {
                DialogControler.Instance.TimeDialog(0);
            }
        }
        else if (timerGame <= 300 && helper[1])
        {
            helper[1] = false;
            if (SceneManager.GetActiveScene().name == "Laboratory")
            {
                bool haveHammer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().viewGlass;
                if (haveHammer)
                {
                    DialogControler.Instance.TimeDialog(1);
                }
                else
                {
                    DialogControler.Instance.TimeDialog(0);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Castle")
            {
                DialogControler.Instance.TimeDialog(0);
            }
            else if (SceneManager.GetActiveScene().name == "Departament")
            {
                DialogControler.Instance.TimeDialog(0);
            }
        }
        else if (timerGame <= 180 && helper[2])
        {
            helper[2] = false;
            if (SceneManager.GetActiveScene().name == "Laboratory")
            {
                bool haveHammer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().viewGlass;
                if (haveHammer)
                {
                    DialogControler.Instance.TimeDialog(1);
                }
                else
                {
                    DialogControler.Instance.TimeDialog(0);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Castle")
            {
                DialogControler.Instance.TimeDialog(0);
            }
            else if (SceneManager.GetActiveScene().name == "Departament")
            {
                DialogControler.Instance.TimeDialog(0);
            }
        }
        else if (timerGame <= 60 && helper[3])
        {
            helper[3] = false;
            if (SceneManager.GetActiveScene().name == "Laboratory")
            {
                bool haveHammer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().viewGlass;
                if (haveHammer)
                {
                    DialogControler.Instance.TimeDialog(1);
                }
                else
                {
                    DialogControler.Instance.TimeDialog(0);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Castle")
            {
                DialogControler.Instance.TimeDialog(0);
            }
            else if (SceneManager.GetActiveScene().name == "Departament")
            {
                DialogControler.Instance.TimeDialog(0);
            }
        }
        else if (timerGame <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
