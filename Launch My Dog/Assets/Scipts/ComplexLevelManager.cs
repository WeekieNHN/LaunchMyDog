using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexLevelManager : MonoBehaviour {

    [Header("ToppingsInLevel")]
    public bool requiredMustard;
    public bool requiredOnions;
    public bool requiredChili;
    public bool requiredRelish;
    public bool requiredSauerkraut;

    [Header("Bun")]
    public GameObject Bun;

    [Header("reset")]

    public bool startRequiredMustard;
    public bool startRequiredOnions;
    public bool startRequiredChili;
    public bool startRequiredRelish;
    public bool startRequiredSauerKraut;

    [Header("Game Managers")]
    public gameManager manager;
    public dogManager dogManager;


    //use this add a specific topping

    public void AddTopping (string Topping)
    {

        if(Topping == "Mustard")
        {

            requiredMustard = false;           
            dogManager.hasMustard = true;

        }

        if (Topping == "Onions")
        {

            requiredOnions = false;
            dogManager.hasOnion = true;

        }

        if (Topping == "Chili")
        {

            requiredChili = false;
            dogManager.hasChili = true;

        }

        if (Topping == "Relish")
        {

            requiredRelish = false;
            dogManager.hasRelish = true;

        }

        if (Topping == "Sauer Kraut")
        {

            requiredSauerkraut = false;
            dogManager.hasSauerKraut = true;

        }

    }

    public void resetbun ()
    {

        Bun.SetActive(false);
        Debug.Log("set the bun to false");
        requiredSauerkraut = startRequiredSauerKraut;
        requiredRelish = startRequiredRelish;
        requiredChili = startRequiredChili;
        requiredMustard = startRequiredMustard;
        requiredOnions = startRequiredOnions;
        Debug.Log("Reset Bools");

    }

	// Use this for initialization
	void Start () {

        startRequiredChili = requiredChili;
        startRequiredMustard = requiredMustard;
        startRequiredOnions = requiredOnions;
        startRequiredRelish = requiredRelish;
        startRequiredSauerKraut = requiredSauerkraut;

	}
	
	// Update is called once per frame
	void Update ()
    {

        //check if bun is ready
        if(requiredMustard == false)
        {

            if (requiredChili == false)
            {

                if (requiredOnions == false)
                {

                    if (requiredRelish == false)
                    {

                        if (requiredSauerkraut == false)
                        {

                            Bun.SetActive(true);

                        }

                    }

                }

            }

        }
        else
        {

            Bun.SetActive(false);

        }

	}
}
