using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogControler : MonoBehaviour
{
    public string[] introDialog;
    public float[] introDelay;
    public AudioClip introAudio;

    public AudioSource audioPlayer;
    public TMP_Text dialogText;

    public GameObject dialogPn;
    public GameObject oniIma;
    public GameObject chloeIma;

    public bool showingDialog;

    public string[] helpDialog;
    public float[] helpDelay;
    public AudioClip[] helpAudios;

    public string[] timeDialog;
    public float[] timeDelay;
    public AudioClip[] timeAudios;

    Dictionary<string, int> specificDialogNum = new Dictionary<string, int>()
    {
        { "Laboratory", 1 },
        { "Castle", 2 },
        { "Departament", 0 },
    };

    public int atMomentDialog;
    public int exceptDialog;
    public int lastDialog;

    public string sceneName;

    public static DialogControler Instance { get; private set; }
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
        audioPlayer = GetComponent<AudioSource>();
        showingDialog = true;
        StartCoroutine(IntroDialog());
        audioPlayer.PlayOneShot(introAudio);
        sceneName = SceneManager.GetActiveScene().name;
        exceptDialog = specificDialogNum[sceneName];
    }

    public void TimeDialog(int timeNum)
    {
        //showingDialog = true;
        StartCoroutine(SpecificTimeDialog(timeNum));
    }

    public void ShowingDialog()
    {
        if (!showingDialog)
        {
            showingDialog = true;
            StartCoroutine(HelpDialog());
        }
        else
        {
        }
    }

    public void ShowingDialog(int helpNum)
    {
        if (!showingDialog)
        {
            showingDialog = true;
            StartCoroutine(SpecificDialog(helpNum));
        }
        else
        {
        }
    }

    void IntroDialogAction(int numDialog)
    {
        if (sceneName == "Laboratory")
        {
            switch (numDialog)
            {
                case 1:
                    ManagerScript.Instance.controlsPn.SetActive(true);
                    break;
                case 3:
                    oniIma.SetActive(false);
                    chloeIma.SetActive(true);
                    break;
                case 4:
                    oniIma.SetActive(true);
                    chloeIma.SetActive(false);
                    break;
            }
        }
        else if (sceneName == "Castle")
        {
            switch (numDialog)
            {
                case 0:
                    oniIma.SetActive(false);
                    chloeIma.SetActive(true);
                    break;
                case 1:
                    oniIma.SetActive(true);
                    chloeIma.SetActive(false);
                    break;
                case 2:
                    oniIma.SetActive(false);
                    chloeIma.SetActive(true);
                    break;
                case 3:
                    oniIma.SetActive(true);
                    chloeIma.SetActive(false);
                    break;
            }
        }
        else if (sceneName == "Departament")
        {
            switch (numDialog)
            {
                case 0:
                    oniIma.SetActive(false);
                    chloeIma.SetActive(true);
                    break;
                case 1:
                    oniIma.SetActive(true);
                    chloeIma.SetActive(false);
                    break;
                case 2:
                    oniIma.SetActive(false);
                    chloeIma.SetActive(true);
                    break;
                case 3:
                    oniIma.SetActive(true);
                    chloeIma.SetActive(false);
                    break;
            }
        }
    }

    IEnumerator IntroDialog()
    {
        dialogPn.SetActive(true);
        for (int i = 0; i < introDialog.Length; i++)
        {
            yield return new WaitForSeconds(introDelay[i]);
            IntroDialogAction(i);
            dialogText.text = introDialog[i];
        }
        ManagerScript.Instance.controlsPn.SetActive(false);
        dialogPn.SetActive(false);
        showingDialog = false;
    }

    IEnumerator HelpDialog()
    {
        do
        {
            atMomentDialog = Random.Range(0, helpDialog.Length - exceptDialog);
        }
        while (lastDialog == atMomentDialog);

        dialogText.text = helpDialog[atMomentDialog];
        oniIma.SetActive(true);
        chloeIma.SetActive(false);
        dialogPn.SetActive(true);
        audioPlayer.PlayOneShot(helpAudios[atMomentDialog]);
        lastDialog = atMomentDialog;
        yield return new WaitForSeconds(helpDelay[atMomentDialog]);
        dialogPn.SetActive(false);
        showingDialog = false;
    }

    IEnumerator SpecificDialog(int dialogNumb)
    {
        dialogText.text = helpDialog[dialogNumb];
        oniIma.SetActive(true);
        chloeIma.SetActive(false);
        dialogPn.SetActive(true);
        audioPlayer.PlayOneShot(helpAudios[dialogNumb]);
        yield return new WaitForSeconds(helpDelay[dialogNumb]);
        dialogPn.SetActive(false);
        showingDialog = false;
        exceptDialog = 0;
    }

    IEnumerator SpecificTimeDialog (int dialogNumb)
    {
        while (showingDialog)
        {
            yield return new WaitForSeconds(1.0f);
        }
        showingDialog = true;
        dialogText.text = timeDialog[dialogNumb];
        oniIma.SetActive(true);
        chloeIma.SetActive(false);
        dialogPn.SetActive(true);
        audioPlayer.PlayOneShot(timeAudios[dialogNumb]);
        yield return new WaitForSeconds(timeDelay[dialogNumb]);
        dialogPn.SetActive(false);
        showingDialog = false;
    }
}
