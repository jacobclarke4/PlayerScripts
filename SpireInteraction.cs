using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SpireInteraction : MonoBehaviour
{
    public GameObject spireSpotLight;
    public GameObject spireLights;
    bool inArea = false;
    public float maxHealth = 50;
    public float currentHealth = 0;
    public HealthBar healthBar;

    public Transform target;
    public GameObject spireDisplay;

    public Animator animCanvas;
    public Animator animUiILight;
    public Animator spireAnim;

    public AudioSource alarmSoundSource;
    public CinemachineImpulseSource spireShake;
    public AudioClip alarmSound;
    public bool disabled = false;
    public GameObject spireParentUFO;
    public AudioSource spireEngagedSoundSource;
    // Start is called before the first frame update
    void Start()
    {
        //uiCanvas.SetActive(false);
        healthBar.SetMaxHealth(50);
        healthBar.SetHealth(currentHealth);
    }
    // Update is called once per frame
    void Update()
    {
        if (inArea == true && disabled == false )
        {
            FaceTarget(spireDisplay);
            //uiCanvas.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                currentHealth += .5f;
                healthBar.SetHealth(currentHealth);
                if(currentHealth == maxHealth)
                {

                    alarmSoundSource.PlayOneShot(alarmSound);
                    spireSpotLight.SetActive(true);
                    spireLights.SetActive(false);
                    LightController.disabledSpires++;
                    if(LightController.disabledSpires == 1)
                    {
                        GameManagerSystem.lastSpireDeactivatedUFO = spireParentUFO;
                    }
                    disabled = true;
                    
                }
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && disabled == false)
        {
            inArea = true;
            animCanvas.SetBool("TurnOn", true);
            animUiILight.SetBool("TurnOn", true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && disabled == true)
        {
            animCanvas.SetBool("TurnOff", true);
            animCanvas.SetBool("TurnOn", false);
            animUiILight.SetBool("TurnOff", true);
            animUiILight.SetBool("TurnOn", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inArea = false;
            animCanvas.SetBool("TurnOff", true);
            animCanvas.SetBool("TurnOn", false);
            animUiILight.SetBool("TurnOff", true);
            animUiILight.SetBool("TurnOn", false);
            //uiCanvas.SetActive(false);
        }
    }
    void FaceTarget(GameObject obj)
    {
        Vector3 direction = (target.position - obj.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * 5);
    }
}
