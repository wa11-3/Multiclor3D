using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator[] animators;
    public float speed = 10.0f;
    public float distance;

    CharacterController controller;

    Outline targetVisual;
    Animator targetAnim;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float moveForward = Input.GetAxis("Vertical");
        float moveSide = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * moveSide + transform.forward * moveForward;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E) && targetVisual != null)
        {
            targetAnim.SetTrigger("Interact");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.gameObject.GetComponent<Outline>() != null)
            {
                targetVisual = hit.collider.gameObject.GetComponent<Outline>();
                targetVisual.enabled = true;
                targetAnim = hit.collider.gameObject.GetComponentInParent<Animator>();
            }
            else
            {
                if (targetVisual != null)
                {
                    targetVisual.enabled = false;
                    targetVisual = null;
                    targetAnim = null;
                }
            }
        }
        else
        {
            if (targetVisual != null)
            {
                targetVisual.enabled = false;
                targetVisual = null;
                targetAnim = null;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.name);
    }
}
