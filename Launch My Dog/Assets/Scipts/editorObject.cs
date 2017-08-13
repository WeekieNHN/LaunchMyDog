using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editorObject : MonoBehaviour {

    [Header("Highlight")]

    private Renderer rend;
    public Color startColor;
    public Color editColor;

    [Header("Editor Variables")]

    public bool isTopping;
    public bool isRevGrav;
    public bool isKetch;

	// Use this for initialization
	void Start ()
    {
        rend = gameObject.GetComponent<Renderer>();
        startColor = rend.material.color;

	}
	
	// Update is called once per frame
	void Update () {

        if (isKetch)
        {

            dripManager ketchScript = gameObject.GetComponent<dripManager>();
            ketchScript.isActivated = false;

        }



	}

    public void startEditing ()
    {

        rend.material.color = editColor;

    }

    public void stopEditing ()
    {

        rend.material.color = startColor;

    }
}
