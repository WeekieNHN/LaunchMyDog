using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dripBehaivor : MonoBehaviour {

    public gameManager manager;

	// Use this for initialization
	void Start () {

        GameObject go = GameObject.FindGameObjectWithTag("GameManager");
        manager = go.GetComponent<gameManager>();

	}
	
	// Update is called once per frame
	void Update () {
		
        if(transform.position.y <= -7)
        {

            Destroy(gameObject);

        }

	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dog")
        {

            manager.resetLevel();

        }
    }
}
