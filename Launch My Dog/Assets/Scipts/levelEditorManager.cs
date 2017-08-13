using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelEditorManager : MonoBehaviour {

    [Header("Currentlevel")]

    public string currentLevelName;
    public bool isSaved;
    public bool hasName;

    [Header("UI")]

    public TextMeshProUGUI nameText;
    public GameObject nameTextGO;
    public InputField nameField;
    public GameObject nameFieldGO;
    public GameObject mainPanel;
    public GameObject editorPanel;
    public GameObject savePanel;
    public GameObject saveQueryPanel;
    public GameObject itemPanel;
    public GameObject showBTN;
    public GameObject editPanel;

    [Header("Items")]

    public GameObject[] itemShowcasePanels;
    public int selectedItem; //array index number
    private int selecteditemCheck;

    public GameObject[] items;

    [Header("Editing Objects")]

    public GameObject objectEditing;
    public GameObject objectEditingCheck;

    [Header("Edit Panel")]

    public RectTransform editPanelTransform;
    public Slider xPosSlider;
    public Slider YPosSlider;
    public Slider RotSlider;
    public InputField XPosField;
    public InputField YPosField;
    public InputField RotField;
    public int axis;
    public int mode;

    public float spinSpeed;
    public bool spin;
    public Toggle spinToggle;
    public Slider spinSpeedSlider;
    public Text SpeedText;

    [Header("Bools")]

    public bool isNameFieldOpen;
    public bool isInEditor;
    public bool itemPanelOpen;
    public bool PlaceMode;
    public bool isEditingObject;
    public bool notFirstObject;
    public bool editPanelOpen;
    public bool isSyncing;

	// Use this for initialization
	void Start () {


        //set up the in editor UI
        nameFieldGO.SetActive(false);
        nameTextGO.SetActive(true);
        showBTN.SetActive(false);

        //set up the panels
        mainPanel.SetActive(true);
        editorPanel.SetActive(false);
        savePanel.SetActive(false);
        saveQueryPanel.SetActive(false);
        itemPanel.SetActive(false);

        //setup bools
        isInEditor = false;

        //setup item selection
        changeSelectedItem();

        //setup edit panel
        editPanelTransform = editPanel.GetComponent<RectTransform>();
        editPanel.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
        if(selectedItem >= 0)
        {

            PlaceMode = true;
            if(editPanelOpen == true)
            {

                deSelectObject();

            }

        }

        else
        {

            PlaceMode = false;


        }

        if(isSaved == false)
        {
            //show that the level needs to be saved
            nameText.text = currentLevelName + "*";

        }

        else
        {
            //show that the level is saved
            nameText.text = currentLevelName;

        }

        if(selectedItem != selecteditemCheck)
        {
            //change selectedItemCheck and update the selected item panel
            changeSelectedItem();

        }

        //
        // Placeing objects in the editor, detect touch position, add z coord. and then instantiate the object;
        //

        //check if editor is open
        if (isInEditor)
        {

            //check to see if itemPanel is closed
            if (PlaceMode == true)
            {

                if (itemPanelOpen == false)
                {

                    for (int i = 0; i < Input.touchCount; ++i)
                    {
                        if (Input.GetTouch(i).phase == TouchPhase.Began)
                        {

                            float x = ((Input.GetTouch(i).position.x * 18.0f) / Screen.width);
                            float y = ((Input.GetTouch(i).position.y * 10.0f) / Screen.height);
                            float z = 4.0f;



                            if (y <= 8.75)
                            {

                                if (y >= 1.25)
                                {
                                    //in the main space
                                    x = (x - 9);
                                    y = (y - 5);
                                    Vector3 v = new Vector3(x, y, z);
                                    Debug.Log("In the grand space");
                                    placeObject(v);

                                }

                            }

                            else if (x <= 15.85)
                            {

                                //around the show button
                                x = (x - 9);
                                y = (y - 5);
                                Vector3 v = new Vector3(x, y, z);
                                Debug.Log("around the show button");
                                placeObject(v);

                            }
                        }
                    }

                }

                //if not factor out the lower 1/3 of the screen
                else
                {

                    for (int i = 0; i < Input.touchCount; ++i)
                    {

                        if (Input.GetTouch(i).phase == TouchPhase.Began)
                        {



                            //convert touch position to world position
                            float x = ((Input.GetTouch(i).position.x * 18.0f) / Screen.width);
                            float y = ((Input.GetTouch(i).position.y * 10.0f) / Screen.height);
                            float z = 4.0f;



                            if (y >= 3.65)
                            {

                                if (y <= 8.75)
                                {
                                    //in the main rectangle
                                    x = (x - 9);
                                    y = (y - 5);
                                    Vector3 v = new Vector3(x, y, z);
                                    Debug.Log("in the space");
                                    placeObject(v);
                                }

                            }

                            else if (y >= 2.5)
                            {

                                if (x <= 15.85)
                                {
                                    //around the hide button
                                    x = (x - 9);
                                    y = (y - 5);
                                    Vector3 v = new Vector3(x, y, z);
                                    Debug.Log("around the hide button");
                                    placeObject(v);

                                }

                            }


                        }

                    }

                }
            }
        }

        if(PlaceMode == false)
        {

            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    RaycastHit hit;

                    

                    // Create a particle if hit
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (editPanelOpen == false)
                        {
                            objectEditing = hit.collider.gameObject;
                            if (notFirstObject == false)
                            {

                                objectEditingCheck = hit.collider.gameObject;
                                notFirstObject = true;

                            }

                            isEditingObject = true;
                            startEditingObject();

                        }

                    }

                    
                        
                }
            }

        }

        if(objectEditingCheck != objectEditing)
        {

            editorObject obj = objectEditingCheck.GetComponent<editorObject>();
            obj.stopEditing();

            objectEditingCheck = objectEditing;
        }


	}

    public void placeObject (Vector3 v)
    {
        if (PlaceMode == true)
        {  

            Instantiate(items[selectedItem], v, Quaternion.identity);

        }
    }

    public void deSelectObject()
    {

        isEditingObject = false;
        stopEditingObject();
        editPanelOpen = false;
        editPanel.SetActive(false);

    }

    public void startEditingObject ()
    {

        editorObject obj = objectEditing.GetComponent<editorObject>();
        obj.startEditing();

        if (objectEditing.transform.position.x <= 0)
        {

            if (objectEditing.transform.position.x <= -3)
            {

                editPanelTransform.anchoredPosition = new Vector2(-100, 40);

            }

            else
            {

                editPanelTransform.anchoredPosition = new Vector2(250, 40);

            }

        }

        if (objectEditing.transform.position.x >= 0)

        {
            if(objectEditing.transform.position.x >= 3)
            {

                editPanelTransform.anchoredPosition = new Vector2(100, 40);

            }

            else
            {

                editPanelTransform.anchoredPosition = new Vector2(-250, 40);

            }

        }

        editPanel.SetActive(true);
        editPanelOpen = true;
        syncEditWindow();

    }

    public void stopEditingObject()
    {

        editorObject obj = objectEditing.GetComponent<editorObject>();
        obj.stopEditing();
        editPanel.SetActive(false);
        editPanelOpen = false;

    }

    public void changeSpinBool ()
    {

        spin = spinToggle.isOn;

        if (spin)
        {

            spinner spinScript = objectEditing.GetComponent<spinner>();
            spinScript.speed = spinSpeed;

        }

        else
        {

            spinner spinScript = objectEditing.GetComponent<spinner>();
            spinScript.speed = 0;

        }

    }

    public void changeSpinSpeed ()
    {

        spinSpeed = spinSpeedSlider.value;
        SpeedText.text = "Speed : " + spinSpeed;

        if (spin)
        {

            spinner spinScript = objectEditing.GetComponent<spinner>();
            spinScript.speed = spinSpeed;

        }

        else
        {

            spinner spinScript = objectEditing.GetComponent<spinner>();
            spinScript.speed = 0;

        }

    }

    public void deleteObjectEditing ()
    {

        Destroy(objectEditing);
        objectEditing = null;
        objectEditingCheck = null;
        notFirstObject = false;
        isEditingObject = false;
        editPanel.SetActive(false);
        editPanelOpen = false;

    }

    public void syncEditWindow ()
    {
        isSyncing = true;
        //set the sliders

        xPosSlider.value = (objectEditing.transform.position.x + 9);
        YPosSlider.value = (objectEditing.transform.position.y + 5);
        RotSlider.value = (objectEditing.transform.rotation.z);

        //set the inputfields
        XPosField.text = "" + (objectEditing.transform.position.x + 9);
        YPosField.text = "" + (objectEditing.transform.position.y + 5);
        RotField.text = "" + (objectEditing.transform.rotation.z);

        if(objectEditing.GetComponent<spinner>().speed != 0)
        {

            spin = true;
            spinToggle.isOn = true;

            spinSpeed = 0;

        }

        isSyncing = false;

    }

    public void setEditingMode (int modeNumber)
    {

        mode = modeNumber;

    }

    public void SetObjectValues (int axis)
    {
        if (isSyncing == false)
        {
            if (mode == 0)
            {
                //take value from slider

                if (axis == 0)
                {

                    //Xpos
                    objectEditing.transform.position = new Vector3((xPosSlider.value - 9), objectEditing.transform.position.y, 4);
                    XPosField.text = "" + (objectEditing.transform.position.x + 9);

                }

                if (axis == 1)
                {

                    //Ypos
                    objectEditing.transform.position = new Vector3(objectEditing.transform.position.x, (YPosSlider.value - 5), 4);
                    YPosField.text = "" + (objectEditing.transform.position.y + 5);

                }

                if (axis == 2)
                {

                    //ZRot
                    objectEditing.transform.eulerAngles = new Vector3(0, 0, RotSlider.value);
                    RotField.text = "" + (objectEditing.transform.rotation.z);

                }

            }

            else if (mode == 1)
            {
                //take value from InputField

                if (axis == 0)
                {

                    //Xpos
                    float x = float.Parse(XPosField.text);

                    if (x < 0)
                    {

                        x = 0;

                    }

                    if (x > 18)
                    {

                        x = 18;

                    }

                    objectEditing.transform.position = new Vector3((x - 9), objectEditing.transform.position.y, 4);
                    xPosSlider.value = (objectEditing.transform.position.x + 9);

                }

                if (axis == 1)
                {

                    //Ypos
                    float y = float.Parse(YPosField.text);

                    if (y < 0)
                    {

                        y = 0;

                    }

                    if (y > 10)
                    {

                        y = 10;

                    }

                    objectEditing.transform.position = new Vector3(objectEditing.transform.position.x, (y - 5), 4);
                    YPosSlider.value = (objectEditing.transform.position.y + 5);

                }

                if (axis == 2)
                {

                    //ZRot
                    float z = float.Parse(RotField.text);
                    if (z < 0)
                    {

                        z = 0;

                    }

                    if (z > 360)
                    {

                        z = 360;

                    }

                    objectEditing.transform.eulerAngles = new Vector3(0, 0, z);
                    RotSlider.value = objectEditing.transform.rotation.z;

                }

            }

        }

    }

    public void changeSelectedItem ()
    {
        //change selectedItemCheck
        selecteditemCheck = selectedItem;

        //de-activate all panels
        foreach(GameObject go in itemShowcasePanels)
        {

            go.SetActive(false);

        }

        //active current panel
        itemShowcasePanels[selectedItem].SetActive(true);

    }

    public void saveLevelButton ()
    {

        saveQueryPanel.SetActive(false);

        if (hasName)
        {
            //if the level has a name

            //save the level

            //open save panel
            savePanel.SetActive(true);
            itemPanel.SetActive(false);
            itemPanelOpen = false;
            nameTextGO.SetActive(false);
            

        }

        if(hasName == false)
        {
            //if the level doesn't have a name
            nameFieldGO.SetActive(true);
            nameTextGO.SetActive(false);
            isNameFieldOpen = true;
            itemPanel.SetActive(false);
            itemPanelOpen = false;
            nameTextGO.SetActive(false);

        }

    }

    public void dontSave ()
    {

        savePanel.SetActive(false);
        saveQueryPanel.SetActive(false);
        itemPanel.SetActive(true);
        showBTN.SetActive(false);
        itemPanelOpen = true;

        GameObject[] itemsToDestroy = GameObject.FindGameObjectsWithTag("item");

        foreach(GameObject go in itemsToDestroy)
        {

            GameObject.Destroy(go);

        }

        notFirstObject = false;

    }

    public void setLevelName ()
    {
        //set the name
        currentLevelName = nameField.text;
        nameFieldGO.SetActive(false);
        nameTextGO.SetActive(true);
        isNameFieldOpen = false;

        //save level

        //set bools
        hasName = true;
        isSaved = true;

        //open save window
        savePanel.SetActive(true);
        itemPanel.SetActive(false);
        itemPanelOpen = false;

        


    }

    public void resumeEditing ()
    {

        savePanel.SetActive(false);
        mainPanel.SetActive(false);
        editorPanel.SetActive(true);
        saveQueryPanel.SetActive(false);
        itemPanel.SetActive(true);
        showBTN.SetActive(false);
        itemPanelOpen = true;
        nameTextGO.SetActive(true);

    }

    public void newLevel ()
    {

        editorPanel.SetActive(true);
        mainPanel.SetActive(false);
        itemPanel.SetActive(true);
        showBTN.SetActive(false);
        itemPanelOpen = true;
        //check if they need to save
        if (isInEditor)
        {

            if(isSaved == false)
            {
                //ask if they would like to save
                saveQueryPanel.SetActive(true);
                showBTN.SetActive(true);
                itemPanel.SetActive(false);
                itemPanelOpen = false;


            }

            else
            {

                resetVariables();
                nameTextGO.SetActive(true);

            }

        }

        else
        {

            resetVariables();
            nameTextGO.SetActive(true);

        }


        

    }

    public void resetVariables ()
    {

        currentLevelName = "";
        isSaved = false;
        hasName = false;
        isInEditor = true;
        itemPanelOpen = true;
        notFirstObject = false;

    }

    public void menuButton ()
    {

        mainPanel.SetActive(true);
        editorPanel.SetActive(false);
        savePanel.SetActive(false);
        saveQueryPanel.SetActive(false);
        isInEditor = false;
        itemPanel.SetActive(false);
        itemPanelOpen = false;
        showBTN.SetActive(false);

        GameObject[] itemsToDestroy = GameObject.FindGameObjectsWithTag("item");

        foreach (GameObject go in itemsToDestroy)
        {

            GameObject.Destroy(go);

        }


    }

    public void selectItem (int item)
    {

        selectedItem = item;

    }

    public void showItemPanel ()
    {

        itemPanel.SetActive(true);
        showBTN.SetActive(false);
        itemPanelOpen = true;
    }

    public void hideItemPanel ()
    {

        itemPanel.SetActive(false);
        showBTN.SetActive(true);
        itemPanelOpen = false;

    }
}
