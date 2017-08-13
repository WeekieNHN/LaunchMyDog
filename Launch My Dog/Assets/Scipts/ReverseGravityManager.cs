using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityManager : MonoBehaviour {

    public GameObject dog;
    public float thrust;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        //when the dog enters the zone 
        if (other.gameObject.tag == "Dog")
        {

            dog = other.gameObject;
            Rigidbody rb = dog.GetComponent<Rigidbody>();
            rb.useGravity = false;

        }

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Dog")
        {

            //when the dog stays in the zone
            dog = other.gameObject;
            Rigidbody rb = dog.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * thrust);

        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Dog")
        {

            //when the dog leaves the zone
            dog = other.gameObject;
            Rigidbody rb = dog.GetComponent<Rigidbody>();
            rb.useGravity = true;

        }
    }
}
