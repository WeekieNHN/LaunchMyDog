using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelectionManager : MonoBehaviour {

    [Header("Panels")]
    public GameObject easyPanel;
    public GameObject normalPanel;
    public GameObject hardPanel;
    public GameObject nextButton;
    public GameObject backButton;
    public int currentpanel;
    public int maxPanel;
    public int minPanel;

    public void nextpanel ()
    {


        currentpanel += 1;

    }

    public void backPanel ()
    {

        currentpanel -= 1;

    }

	// Use this for initialization
	void Start () {

        currentpanel = 1;
        minPanel = 1;

	}
	
	// Update is called once per frame
	void Update () {
		
        if(currentpanel == maxPanel)
        {

            nextButton.SetActive(false);

        }

        else
        {

            nextButton.SetActive(true);

        }

        if(currentpanel == minPanel)
        {

            backButton.SetActive(false);

        }

        else
        {

            backButton.SetActive(true);

        }

        if(currentpanel == 1)
        {

            easyPanel.SetActive(true);
            normalPanel.SetActive(false);
            hardPanel.SetActive(false);

        }

        if (currentpanel == 2)
        {

            easyPanel.SetActive(false);
            normalPanel.SetActive(true);
            hardPanel.SetActive(false);

        }

        if (currentpanel == 3)
        {

            easyPanel.SetActive(false);
            normalPanel.SetActive(false);
            hardPanel.SetActive(true);

        }

    }
}
