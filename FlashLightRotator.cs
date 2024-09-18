using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightRotator : MonoBehaviour
{
    private Vector3 v3Offset;
    private GameObject goFollow;
    private float speed = 2f;
    public GameObject flashPos;
    public GameObject lightPos;
    void Start () 
    {
         goFollow = Camera.main.gameObject;
         v3Offset = transform.position - goFollow.transform.position;
         transform.LookAt(lightPos.transform.position);
    }
     
    void Update () 
    {
        //transform.position = goFollow.transform.position + v3Offset;
         transform.position = flashPos.transform.position;
         transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
         //transform.LookAt(lightPos.transform.position);
    }
}
