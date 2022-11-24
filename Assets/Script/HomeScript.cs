using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HomeScript : MonoBehaviour
{
    public TextMeshProUGUI pressTx;

    Color newcolor = new Color(0, 0, 0, 0);

    bool spaceAct = true;

    private void Start()
    {
        StartCoroutine(SpaceLoop());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (SceneManager.GetActiveScene().name == "Home")
            {
                SceneManager.LoadScene("Intro");
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    IEnumerator SpaceLoop()
    {
        while (true)
        {
            spaceAct = !spaceAct;
            yield return new WaitForSeconds(1.0f);

            if (spaceAct)
            {
                for (float i = 0; i < 1; i += 0.01f)
                {
                    newcolor.a = i;
                    pressTx.color = newcolor;
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                for (float i = 1; i > 0; i -= 0.01f)
                {
                    newcolor.a = i;
                    pressTx.color = newcolor;
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }

    }
}
