using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Animator[] animators;
    public float speed = 10.0f;
    public float distance;

    public GameObject showCase;

    public Material brokenGlass;

    CharacterController controller;

    GameObject colorObject;
    GameObject roomObject;
    Outline targetVisual;
    Animator targetAnim;

    bool haveHammer = false;
    bool glassBroken = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        colorObject = GameObject.FindGameObjectWithTag("ColorObject");
    }

    private void Update()
    {
        if (!ManagerScript.Instance.shoowingOption)
        {
            if (Input.GetMouseButtonDown(0) && targetVisual != null)
            {
                if (roomObject.CompareTag("Hammer"))
                {
                    Destroy(roomObject);
                    roomObject = null;
                    haveHammer = true;
                }
                else if (roomObject.CompareTag("GlassLab"))
                {
                    if (haveHammer && !glassBroken)
                    {
                        roomObject.GetComponent<MeshRenderer>().material = brokenGlass;
                        glassBroken = true;
                    }
                    else if (glassBroken)
                    {
                        Destroy(colorObject);
                    }
                    else
                    {
                        DialogControler.Instance.ShowingDialog(4);
                    }
                }
                else
                {
                    targetAnim.SetTrigger("Interact");
                }
            }

            float moveForward = Input.GetAxis("Vertical");
            float moveSide = Input.GetAxis("Horizontal");

            Vector3 move = transform.right * moveSide + transform.forward * moveForward;

            controller.Move(move * speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            roomObject = hit.collider.gameObject;

            if (hit.collider.gameObject.GetComponent<Outline>() != null)
            {
                targetVisual = roomObject.GetComponent<Outline>();
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
            if (targetVisual != null && SceneManager.GetActiveScene().name == "Laboratory")
            {
                targetVisual.enabled = false;
                showCase.GetComponent<Outline>().enabled = false;
                targetVisual = null;
                targetAnim = null;
            }
        }
    }
}
