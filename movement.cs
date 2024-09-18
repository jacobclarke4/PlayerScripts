using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    float gravity = -9.81f;
    public float sprintSpeed = 20f;
    public float jumpHeight = 2f;
    Vector3 velocity;
    bool isInAir = false;
    float fallTimer = 0f;
    public Animator anim;
    public string animState = "Idle";
    public List<string> animList = new List<string> { "Idle", "Run", "Wake"};
    public GameObject cam;
    Vector3 camPos;
    public GameObject flashPos;
    bool wakeUpCheck = false;
    public GameObject cameraResetPos;
    public static bool moveBool = false;
    float wakeTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        animState = "Wake";
        //camPos = cam.transform.position;
        
        
    }

    // Update is called once per frame
    void Update()
    { 
        
       if(GameManagerSystem.wakeUp == true)
        {
            anim.enabled = true;
            animState = "Wake";
        }
       
        if (animState == "Wake" && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && GameManagerSystem.wakeUp == true)
        {
            //cam.transform.SetParent(gameObject.transform);
            //camPos = cam.transform.position;
            wakeUpCheck = true;
            moveBool = true;
            mouseMovement.mouseBool = true;

            animState = "Idle";
            
            //cam.GetComponent<HeadBob>().enabled = true;
            //cam.transform.position = camPos;

        }
        /*
        if(wakeUpCheck == true)
        {
            cam.transform.SetParent(gameObject.transform);
            cam.transform.localPosition = new Vector3(0, cameraResetPos.transform.localPosition.y, 0);
        }
        */
        //Debug.Log("wakeUpCheck: " + wakeUpCheck);
        /*
        if(findWeapon() && anim.GetBool("Equipped") == false)
        {
            anim.SetBool("Equipped", true);
        }
        else
        {
            anim.SetBool("Equipped", false);
        }
        */
        if (moveBool == true)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (controller.isGrounded)
            {
                fallTimer = 0;
                if (Input.GetButtonDown("Jump"))
                {

                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
            if (controller.isGrounded && velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            if (wakeUpCheck == true && x == 0f && z == 0f)
            {
                speed = 0f;
                animState = "Idle";
                //cam.GetComponent<HeadBob>().bobbingAmount = .002f;
                //cam.GetComponent<HeadBob>().walkingBobbingSpeed = 6;
                //flashPos.GetComponent<HeadBob>().bobbingAmount = .002f;
                //flashPos.GetComponent<HeadBob>().walkingBobbingSpeed = 6;
            }
            else if (Input.GetKey(KeyCode.LeftShift) && wakeUpCheck == true)
            {
                speed = sprintSpeed;
                animState = "Run";
                //cam.GetComponent<HeadBob>().bobbingAmount = .002f;
                //cam.GetComponent<HeadBob>().walkingBobbingSpeed = 15;
                flashPos.GetComponent<HeadBob>().bobbingAmount = .002f;
                flashPos.GetComponent<HeadBob>().walkingBobbingSpeed = 15;
            }
            else
            {
                speed = 6f;
                animState = "Idle";
            }

            velocity.y += gravity * Time.deltaTime;
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            controller.Move(velocity * Time.deltaTime);
        }
        animationControls();
    }


    public void animationControls()
    {
        for (int i = 0; i < animList.Count; i++)
        {

            if (animState.Equals(animList[i]))
            {
                //Debug.Log("TRUE "  + animState);
                anim.SetBool(animState, true);

            }
            else
            {
                //Debug.Log("FALSE " + animList[i]);
                anim.SetBool(animList[i], false);
            }
        }
    }
}
