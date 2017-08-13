using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dripManager : MonoBehaviour {

    public GameObject dripObject;
    public float frequency;
    public float timer = 0.0f;
    public Transform spawnPos;
    public gameManager Manager;
    public bool isActivated = true;


	// Use this for initialization
	void Start () {

        timer = 0.0f;

	}

    public void drip ()
    {
        //set drip pos
        if (isActivated)
        {

            Vector3 dripPos = new Vector3(spawnPos.position.x, spawnPos.position.y, spawnPos.position.z);

            //drip a drip
            GameObject go = (GameObject)Instantiate(dripObject, dripPos, Quaternion.identity);

            //set variables
            dripBehaivor godb = go.AddComponent<dripBehaivor>();
            godb.manager = Manager;

            //reset timer
            timer = 0.0f;

        }

    }
	
	// Update is called once per frame
	void Update () {

        //update timer
        timer += Time.deltaTime;


        //check if drip should drip a drip
        if(timer >= frequency)
        {

            drip();

        }

	}
}
