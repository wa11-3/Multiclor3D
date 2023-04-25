using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Intro : MonoBehaviour
{
    public string[] dialog;
    public float[] delay;

    public TextMeshProUGUI dialgText;

    private void Start()
    {
        StartCoroutine(ShowDialog());
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
