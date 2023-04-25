using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField]
    GameObject photoBG;
    [SerializeField]
    Image photoArea;

    Texture2D screenCapture;
    CamScript camScript;

    bool shoowingPhoto;
    public bool shoowingOption;
    bool shoowingControls;
    bool shoowingMachine;

    public GameObject optionPn;
    public GameObject controlsPn;

    public GameObject machine;
    public TMP_Text machineTx;

    public static ManagerScript Instance { get; private set; }
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
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        camScript = Camera.main.GetComponent<CamScript>();
        shoowingPhoto = false;
        shoowingOption = false;
        shoowingMachine = false;
    }

    private void Update()
    {
        if (shoowingMachine)
        {
            machineTx.text = TimerController.Instance.TimerManager();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!shoowingMachine)
            {
                machine.SetActive(true);
                shoowingMachine = true;
            }
            else
            {
                machine.SetActive(false);
                shoowingMachine = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DialogControler.Instance.ShowingDialog();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!shoowingPhoto)
            {
                StartCoroutine(Capture());
                shoowingPhoto = true;
            }
            else
            {
                photoBG.SetActive(false);
                shoowingPhoto = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (shoowingControls)
            {
                controlsPn.SetActive(false);
                shoowingControls = false;
            }
            else if (!shoowingOption)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                shoowingOption = true;
                optionPn.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                shoowingOption = false;
                optionPn.SetActive(false);
            }
        }
    }

    IEnumerator Capture()
    {
        camScript.enabled = false;
        yield return new WaitForEndOfFrame();

        Rect regiontoRead = new Rect(0f, 0f, Screen.width, Screen.height);

        screenCapture.ReadPixels(regiontoRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
        camScript.enabled = true;
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0f, 0f, Screen.width, Screen.height), new Vector2(0.5f, 0.5f), 100f);
        photoArea.sprite = photoSprite;
        photoBG.SetActive(true);
    }

    #region OptionsBts
    public void OnClickControls()
    {
        controlsPn.SetActive(true);
        shoowingControls = true;
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
    #endregion

}
