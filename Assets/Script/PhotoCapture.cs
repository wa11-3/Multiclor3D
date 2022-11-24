using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField]
    Image photoArea;

    Texture2D screenCapture;

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    private void Update()
    {
        if (Input.GetKey("p"))
        {
            Debug.Log("Tomando foto");
            StartCoroutine(Capture());
        }
    }

    IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();

        Rect regiontoRead = new Rect(0f, 0f, Screen.width, Screen.height);

        screenCapture.ReadPixels(regiontoRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0f, 0f, Screen.width, Screen.height), new Vector2(0.5f, 0.5f), 100f);
        photoArea.sprite = photoSprite;
    }
}
