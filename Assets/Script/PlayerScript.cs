using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Animator[] animators;
    public float speed = 10.0f;
    public float distance;

    public GameObject showCase;

    public Material brokenGlass;
    public Material blueMaterial;

    CharacterController controller;

    GameObject colorObject;
    GameObject roomObject;
    Outline targetVisual;
    Animator targetAnim;

    bool haveHammer = false;
    bool glassBroken = false;

    public bool viewGlass;

    int greenPiece = 0;
    int lightObject = 23;

    CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        controller = GetComponent<CharacterController>();
        colorObject = GameObject.FindGameObjectWithTag("ColorObject");
        if (SceneManager.GetActiveScene().name == "Laboratory")
        {
            viewGlass = false;
        }
        else if (SceneManager.GetActiveScene().name == "Castle")
        {
            blueMaterial.color = new Color(1, 1, 1);
            StartCoroutine(DialogHelp(40.0f, 4));
            StartCoroutine(DialogHelp(45.0f, 5));
        }
        else if (SceneManager.GetActiveScene().name == "Departament")
        {
            StartCoroutine(DialogHelp(20.0f, 4));
        }
    }

    private void Update()
    {
        if (!ManagerScript.Instance.shoowingOption)
        {
            if (Input.GetMouseButtonDown(0) && targetVisual != null)
            {
                ObjectsController();
            }

            float moveForward = Input.GetAxis("Vertical");
            float moveSide = Input.GetAxis("Horizontal");

            Vector3 move = transform.right * moveSide + transform.forward * moveForward;

            controller.Move(move * speed * Time.deltaTime);
        }

        //if (lightObject <= 0)
        //{
        //    colorObject.GetComponent<MeshRenderer>().material = blueMaterial;
        //    colorObject.AddComponent<Outline>();
        //}

        characterController.Move(new Vector3(0, -0.1f, 0));
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
            if (targetVisual != null)
            {
                targetVisual.enabled = false;
                targetVisual = null;
                targetAnim = null;
                if (SceneManager.GetActiveScene().name == "Laboratory")
                {
                    showCase.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }

    private void ObjectsController()
    {
        if (roomObject.CompareTag("Hammer"))
        {
            Destroy(roomObject);
            roomObject = null;
            haveHammer = true;
        }
        else if (roomObject.CompareTag("GlassLab"))
        {
            viewGlass = true;
            if (haveHammer && !glassBroken)
            {
                roomObject.GetComponent<MeshRenderer>().material = brokenGlass;
                glassBroken = true;
            }
            else if (glassBroken)
            {
                Destroy(colorObject);
                StartCoroutine(GotoScene("Castle"));
            }
            else
            {
                DialogControler.Instance.ShowingDialog(4);
            }
        }
        else if (roomObject.CompareTag("LightObject"))
        {
            Destroy(roomObject.transform.parent.gameObject);
            blueMaterial.color = new Color(blueMaterial.color.r - 0.0431f, blueMaterial.color.g - 0.0431f, blueMaterial.color.b);
            lightObject -= 1;
        }
        else if (roomObject.CompareTag("GreenPiece"))
        {
            Destroy(roomObject);
            greenPiece += 1;

            if (greenPiece == 7)
            {
                //speed = 0;
                //GetComponentInChildren<CameraScript>().enabled = false;
                //ManagerScript manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerScript>();
                //manager.ShowLastChoice();
                SceneManager.LoadScene("Home");
            }
        }
        else if (roomObject.CompareTag("ColorObject"))
        {
            if (SceneManager.GetActiveScene().name == "Castle")
            {
                Destroy(roomObject.transform.parent.gameObject);
                StartCoroutine(GotoScene("Departament"));
            }
        }
        else
        {
            targetAnim.SetTrigger("Interact");
        }
    }

    IEnumerator GotoScene(string sceneName)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator DialogHelp(float waitTime,int numDialog)
    {
        yield return new WaitForSeconds(waitTime);
        DialogControler.Instance.ShowingDialog(numDialog);
    }
}
