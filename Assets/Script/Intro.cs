using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Image skipIM;

    public string[] dialog;
    public float[] delay;

    public TextMeshProUGUI dialgText;

    private void Start()
    {
        StartCoroutine(ShowDialog());
    }

    private void Update()
    {
        if (skipIM.fillAmount >= 1f)
        {
            SceneManager.LoadScene("Laboratory");
        }

        if (Input.anyKey)
        {
            skipIM.fillAmount += 0.01f;
        }
        else
        {
            skipIM.fillAmount = 0f;
        }
    }

    public void EndIntro()
    {
        SceneManager.LoadScene("Laboratory");
    }

    IEnumerator ShowDialog()
    {
        for (int i = 0; i < dialog.Length; i++)
        {
            yield return new WaitForSeconds(delay[i]);
            dialgText.text = dialog[i];
        }
    }
}
