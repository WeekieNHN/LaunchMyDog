﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingPickupManager : MonoBehaviour {

    public float speed;
    public string toppingName;
    public ComplexLevelManager manager;

	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame

	void Update () {

        transform.Rotate(Vector3.back * speed * Time.deltaTime);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dog")
        {

            manager.AddTopping(toppingName);

            gameObject.SetActive(false);

        }
    }
}