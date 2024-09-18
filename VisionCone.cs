using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public bool IseeYou = false;
    public float iDontSeeYouTimer = 0f;
    public bool spotted = false; //spotted is used so iSeeYouTimer is not running before alien spotted player 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spotted == true && IseeYou == false)
        {
            iDontSeeYouTimer += Time.deltaTime;
            if(iDontSeeYouTimer > 10f)
            {
                spotted = false;
                iDontSeeYouTimer = 0f;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
        {
            iDontSeeYouTimer = 0;
            spotted = true;
            Debug.Log("I SEE U");
            IseeYou = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
            Debug.Log("BYE BYE");
            IseeYou = false;
        }
    }
}
