using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonTEST : MonoBehaviour
{

    GameObject Good1;
    
    void Start()
    {
        Good1 = GameObject.Find("Good1"); //找到Good1
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hi, 123");
        Rigidbody Good1rb = Good1.GetComponent<Rigidbody>();
        Good1rb.isKinematic = true;
        Good1rb.detectCollisions = false;
    }
    }
