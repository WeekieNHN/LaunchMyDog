using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogManager : MonoBehaviour {

    [Header("Toppings")]
    public GameObject mustardTopping;
    public GameObject relishTopping;
    public GameObject chiliTopping;
    public GameObject OnionTopping;
    public GameObject sauerKrautTopping;

    [Header("Bools")]
    public bool hasMustard;
    public bool hasRelish;
    public bool hasChili;
    public bool hasOnion;
    public bool hasSauerKraut;

    [Header("GameM Manager")]
    public gameManager manager;

	// Use this for initialization
	void Start () {
		


	}

    private void OnCollisionEnter(Collision collision)
    {
        
        

    }

    // Update is called once per frame
    void Update () {
		
        //mustard

        if(hasMustard == true)
        {

            mustardTopping.SetActive(true);

        }

        else
        {

            mustardTopping.SetActive(false);

        }

        //Onions

        if (hasOnion == true)
        {

            OnionTopping.SetActive(true);

        }

        else
        {

            OnionTopping.SetActive(false);

        }

        //Chili

        if (hasChili == true)
        {

            chiliTopping.SetActive(true);

        }
        else

        {

            chiliTopping.SetActive(false);

        }

        //Relish

        if (hasRelish == true)
        {

            relishTopping.SetActive(true);

        }
        else

        {

            relishTopping.SetActive(false);

        }

        //Sauer Kraut

        if (hasSauerKraut == true)
        {

            sauerKrautTopping.SetActive(true);

        }
        else

        {

            sauerKrautTopping.SetActive(false);

        }

    }


    public void resetDog ()
    {

        OnionTopping.SetActive(false);
        relishTopping.SetActive(false);
        chiliTopping.SetActive(false);
        mustardTopping.SetActive(false);
        sauerKrautTopping.SetActive(false);
        hasOnion = false;
        hasRelish = false;
        hasMustard = false;
        hasChili = false;
        hasSauerKraut = false;

    }

    public void OnHitByDrip ()
    {



    }
}
