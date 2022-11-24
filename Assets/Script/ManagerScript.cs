using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        camScript = Camera.main.GetComponent<CamScript>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Tomando foto");
            StartCoroutine(Capture());
        }
    }

    IEnumerator Capture()
    {
        camScript.enabled = false;
        yield return new WaitForEndOfFrame();

        Rect regiontoRead = new Rect(0f, 0f, Screen.width, Screen.height);

        screenCapture.ReadPixels(regiontoRead, 0, 0, false);
        screenCapture.Apply();
        camScript.enabled = true;
        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0f, 0f, Screen.width, Screen.height), new Vector2(0.5f, 0.5f), 100f);
        photoArea.sprite = photoSprite;
        photoBG.SetActive(true);
    }

}
