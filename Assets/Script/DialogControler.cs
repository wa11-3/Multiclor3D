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

    Dictionary<string, int> specificDialogNum = new Dictionary<string, int>()
    {
        { "Laboratory", 1 },
    };

    public int atMomentDialog;
    public int exceptDialog;
    public int lastDialog;

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
        showingDialog = false;
        //StartCoroutine(IntroDialog());
        //audioPlayer.PlayOneShot(introAudio);
        exceptDialog = specificDialogNum[SceneManager.GetActiveScene().name];
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

    IEnumerator IntroDialog()
    {
        dialogPn.SetActive(true);
        for (int i = 0; i < introDialog.Length; i++)
        {
            yield return new WaitForSeconds(introDelay[i]);

            switch (i)
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
}
