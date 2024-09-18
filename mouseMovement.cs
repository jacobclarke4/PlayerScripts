using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class mouseMovement : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform player;
    float xRotation = 0f;
    public Ray ray;
    public RaycastHit hit;
    //public CinemachineImpulseSource spaceShip;
    public bool alienSpotted;
    GameObject alien = null;
    public static bool mouseBool = false;
    public GameObject lightPos;
    Quaternion playerStartRotation;
    bool rotationCheck = false;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        alienSpotted = false;
        player.gameObject.GetComponentInChildren<Light>().transform.LookAt(lightPos.transform.position);
        playerStartRotation = transform.parent.transform.rotation;
    }

    // Update is called once per frame

    void Update()
    {
        if (mouseBool == false)
        {

        }
        if(mouseBool == true)
        {
            

           
            if(rotationCheck == true)
            {
                float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                //flasLight.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                player.Rotate(Vector3.up * mouseX);
                rayCastCheck();
            }
            else
            {
                if (timer < 1.0f)
                {
                    timer += Time.deltaTime / 3;
                    transform.rotation = Quaternion.Slerp(transform.rotation, playerStartRotation, timer);
                }
                else
                {
                    transform.rotation = playerStartRotation;
                    rotationCheck = true;
                    
                }
                
            }
           

        }
    }

    void rayCastCheck()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Alien")
            {
               
                if (alienSpotted == false)
                {
                    Debug.Log("ALIEN!");
                    //spaceShip.GenerateImpulse();
                    alienSpotted = true;
                }
            }   
            if (hit.collider.tag == "Enemy" && Vector3.Distance(hit.collider.gameObject.transform.position, transform.position) < 30f)
            {
                Debug.Log("ENEMY");
                alien = hit.collider.gameObject;
                //hit.collider.gameObject.GetComponent<Animator>().SetBool("Duck", true);
                //hit.collider.gameObject.GetComponent<EnemyAI>().animState = "Duck";
                hit.collider.gameObject.transform.parent.GetComponent<Animator>().SetBool("RayCast", true);
                alienSpotted = true;

            }
            else
            {
                alienSpotted = false;
                if(alien != null)
                {
                    alien.transform.parent.gameObject.GetComponent<Animator>().SetBool("RayCast", false);
                    alien = null;
                }
                
            }

            
        }
        else if (alienSpotted == true)
        {
            alienSpotted = false;
            if (alien != null)
            {
                alien.transform.parent.gameObject.GetComponent<Animator>().SetBool("RayCast", false);
                alien = null;
            }
        }
    }
}
