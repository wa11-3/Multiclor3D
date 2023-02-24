using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Vector2 mouseView;
    Vector2 smoothVelocity;

    public float sensibility;
    public float smooth;

    GameObject player;

    private void Start()
    {
        player = this.transform.parent.gameObject;
    }

    private void Update()
    {
        if (!ManagerScript.Instance.shoowingOption)
        {
            Vector2 movement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            movement = Vector2.Scale(movement, new Vector2(smooth * sensibility, smooth * sensibility));

            smoothVelocity.x = Mathf.Lerp(smoothVelocity.x, movement.x, 1.0f / smooth);
            smoothVelocity.y = Mathf.Lerp(smoothVelocity.y, movement.y, 1.0f / smooth);

            mouseView += smoothVelocity;
            mouseView.y = Mathf.Clamp(mouseView.y, -90.0f, 90.0f);
            transform.localRotation = Quaternion.AngleAxis(-mouseView.y, Vector3.right);
            player.transform.localRotation = Quaternion.AngleAxis(mouseView.x, Vector3.up);
        }
    }
}
