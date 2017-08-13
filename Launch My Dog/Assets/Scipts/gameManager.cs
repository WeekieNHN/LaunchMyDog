using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    [Header("Game")]

    public int gameState;//0 is boot/mainmenu, 1 is playing, 2 is level end, 3 is settings, 4 is level selection
    public bool isInPlay;

    
    [Header("Levels")]

    public int currentLevel;
    public TextMeshProUGUI TitleText;
    public GameObject CurrentLevelObject;


    //Each level should be it's own parent game object w/ obstacles as child objects
    public GameObject[] levels;

    [Header("During Play")]

    public GameObject hideOnPlay;

    public GameObject[] Toppings;

    [Header("Pause")]

    public bool Paused;

    [Header("UI")]

    public GameObject endArrow;
    public GameObject gameCanvas;
    public GameObject menuCanvas;
    public GameObject PauseCanvas;
    public GameObject levelSelectionMenu;
    public GameObject settingsMenu;
    public GameObject EndLevelCanvas;
    public GameObject GameEndCanvas;

    public GameObject endArrowGO;

    public GameObject[] Locks;

    [Header("Launching")]

    public float launchVelocity;
    public GameObject hotDogArrow;
    public Transform hotDogTransform;
    public GameObject hotDogObject;
    public int maxAngle;
    public int minAngle;
    public int currentAngle;

    [Header("Hand")]

    public GameObject hand;
    public Animator handAnim;

    [Header("Settings")]

    public Toggle SFXToggle;
    public Toggle MusicToggle;
    public int SFXToggleInt;
    public int MusicToggleInt;

    [Header("Reset")]

    public GameObject ResetButton;
    public Toggle ResetToggle;

    [Header("End Game")]

    public GameObject titleSMW;
    public GameObject titleLMD;
    public GameObject launchText;
    public GameObject squeezeText;
    public Toggle titleToggleTG;
    public bool titleToggleBool;

    [Header("Sound")]

    public AudioSource _source;

    public AudioClip[] buttonClip;
    public AudioClip upClip;
    public AudioClip downClip;
    public AudioClip fail;

    [Header("Level Editor")]

    public GameObject EditorCanvas;

    [Header("Bools")]

    public bool sfxEnabled;
    public bool musicEnabled;

    [Header("Manager")]

    public dogManager dogManager;

    //Social media Links

    public void YouTube ()
    {

        Application.OpenURL("https://www.youtube.com/channel/UCpURw2zFdFp81UWlfRXXAHQ");

    }

    public void facebook ()
    {

        Application.OpenURL("https://www.facebook.com/PokeMyBalls/");

    }

    public void Twitter ()
    {

        Application.OpenURL("https://twitter.com/NerdHerdNetwork");

    }

    public void Instagram ()
    {

        Application.OpenURL("https://www.instagram.com/nerdherdnetwork/");

    }

    public void buttonSound ()
    {
        _source.volume = 0.6f;
        _source.clip = buttonClip[Random.Range(0, buttonClip.Length)];
        if (sfxEnabled)
        {

            _source.Play();

        }
    }

    public void back ()
    {
        Time.timeScale = 1;
        gameState = 0;
        menuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        levelSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);
        PauseCanvas.SetActive(false);
        EndLevelCanvas.SetActive(false);
        GameEndCanvas.SetActive(false);
        EditorCanvas.SetActive(false);
        isInPlay = false;

    }

    // Use this for initialization

    void Start()
    {
        // set up UI

        menuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        levelSelectionMenu.SetActive(false);
        PauseCanvas.SetActive(false);
        settingsMenu.SetActive(false);
        EndLevelCanvas.SetActive(false);
        EditorCanvas.SetActive(false);

        //Set level and state

        currentLevel = 1;
        gameState = 0;

        //get hands for animation

        handAnim = hand.GetComponent<Animator>();

        //set settings values

        SFXToggleInt = PlayerPrefs.GetInt("SFX");
        MusicToggleInt = PlayerPrefs.GetInt("Music");

        //print highestlevel value

        Debug.Log(PlayerPrefs.GetInt("HighestLevel"));

        //update locks based on that value

        UpdateLevelLocks();

        if (MusicToggleInt == 0)
        {

            MusicToggle.isOn = false;
            musicEnabled = true;
        
        }

        if(MusicToggleInt == 1)
        {
            MusicToggle.isOn = true;
            musicEnabled = true;

        }

        if(SFXToggleInt == 0)
        {

            SFXToggle.isOn = false;
            sfxEnabled = false;

        }

        if(SFXToggleInt == 1)
        {

            SFXToggle.isOn = true;
            sfxEnabled = true;

        }

        

    }

    public void upSound ()
    {

        _source.clip = upClip;
        _source.volume = 0.4F;
        if (sfxEnabled)
        {

            _source.Play();

        }

    }

    public void downSound ()
    {

        _source.clip = downClip;
        _source.volume = 0.4f;
        if (sfxEnabled)
        {

            _source.Play();

        }

    }

    // Update is called once per frame

    void Update()
    {
        if(currentLevel >= 28)
        {

            endArrowGO.SetActive(false);

        }

        else
        {

            endArrowGO.SetActive(true);

        }

        if(SFXToggle.isOn == true)
        {

            SFXToggleInt = 1;
            PlayerPrefs.SetInt("SFX", SFXToggleInt);
            sfxEnabled = true;

        }

        if(SFXToggle.isOn == false)
        {

            SFXToggleInt = 0;
            PlayerPrefs.SetInt("SFX", SFXToggleInt);
            sfxEnabled = false;

        }

        if(MusicToggle.isOn == true)
        {

            MusicToggleInt = 1;
            PlayerPrefs.SetInt("Music", MusicToggleInt);
            musicEnabled = true;

        }

        if(MusicToggle.isOn == false)
        {

            MusicToggleInt = 0;
            PlayerPrefs.SetInt("Music", MusicToggleInt);
            musicEnabled = false;

        }

        //check for if dog missed

        if(hotDogTransform.position.y <= -7)
        {

            if(isInPlay == true)
            {

                //play the fail sound
                _source.clip = fail;
                _source.volume = 0.5f;
                if (sfxEnabled)
                {

                    _source.Play();

                }

                resetLevel();

            }

        }

        //reset game button

        if(ResetToggle.isOn == true)
        {

            ResetButton.SetActive(true);

        }

        else

        {

            ResetButton.SetActive(false);

        }

    }

    public void SettingsMenu ()
    {

        menuCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        levelSelectionMenu.SetActive(false);
        settingsMenu.SetActive(true);

    }
    
    public void editor ()
    {

        menuCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        levelSelectionMenu.SetActive(false);
        PauseCanvas.SetActive(false);
        settingsMenu.SetActive(false);
        EndLevelCanvas.SetActive(false);
        EditorCanvas.SetActive(true);

        foreach (GameObject go in levels)
        {

            go.SetActive(false);

        }

    }

    public void ChangeLevel (int levelToLoad)
    {
        //deactivate all levels
        currentLevel = levelToLoad;
        foreach(GameObject go in levels)
        {

            go.SetActive(false);

        }

        Debug.Log("Deactivated all objects");

        //get next level array element

        int ArrayElementToLoad = levelToLoad - 1;

        //check if it exists

        int arrayLength;
        arrayLength = levels.Length;

        if(arrayLength < currentLevel)
        {

            //if it doesnt exist, load menu that explains why

            gameCanvas.SetActive(false);
            GameEndCanvas.SetActive(true);
            return;
        }

        else
        {

            //if it exists, load it

            levels[ArrayElementToLoad].SetActive(true);

        }

        //close / load menus

        levelSelectionMenu.SetActive(false);
        EndLevelCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        hideOnPlay.SetActive(true);
        TitleText.text = "Level " + levelToLoad;

        foreach (GameObject go in Toppings)
        {

            go.SetActive(true);

        }

        //reset play session
        resetLevel();

    }

    public void resetGameStats ()
    {

        //Resets locking

        PlayerPrefs.SetInt("HighestLevel", 0);
        UpdateLevelLocks();


    }

    public void resetLevel ()
    {
        Time.timeScale = 1;
        hideOnPlay.SetActive(true);
        hotDogArrow.SetActive(true);
        Paused = false;
        PauseCanvas.SetActive(false);
        EndLevelCanvas.SetActive(false);
        Vector3 resetPosition = new Vector3(-7.5f, -4, 0);
        Quaternion resetRotation = Quaternion.Euler(0, 0, 125);
        hotDogTransform.position = resetPosition;
        hotDogTransform.rotation = resetRotation;
        Rigidbody rb = hotDogObject.GetComponent<Rigidbody>();
        Destroy(rb);
        isInPlay = false;
        currentAngle = 125;
        gameCanvas.SetActive(true);

        //reset toppings

        foreach(GameObject go in Toppings)
        {

            go.SetActive(true);

        }

        //reset dog

        dogManager.resetDog();

        //reset the bun

        GameObject LevelObject;
        int levelToResetBun;
        levelToResetBun = (currentLevel - 1);
        Debug.Log("Currentlevel" + currentLevel + " / array" + levelToResetBun);
        LevelObject = levels[levelToResetBun];
        ComplexLevelManager currentlevelManager;

        currentlevelManager = LevelObject.GetComponent<ComplexLevelManager>();
        if (currentLevel >= 10)
        {

            currentlevelManager.resetbun();
            Debug.Log("Reset bun");
        }
    }

    public void PauseGame ()
    {
        //check for if already pause
        if(Paused == true)
        {
            //what to do when it's paused
            Paused = false;
            PauseCanvas.SetActive(false);
            if(isInPlay == true)
            {

                hideOnPlay.SetActive(false);

            }
            else
            {

                hideOnPlay.SetActive(true);

            }
            Time.timeScale = 1;

        }
        else
        {
            //what to do when it's not paused
            Paused = true;
            PauseCanvas.SetActive(true);
            hideOnPlay.SetActive(false);
            Time.timeScale = 0;
        }


    }

    public void playButton ()
    {

        menuCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        levelSelectionMenu.SetActive(true);
        gameState = 4;

    }

    public void UpArrow ()
    {
        
        if (currentAngle < maxAngle)
        {

            hotDogTransform.Rotate(0, 0, 5);
            currentAngle += 5;

        }

    }

    public void DownArrow ()
    {

        if (currentAngle > minAngle)
        {

            hotDogTransform.Rotate(0, 0, -5);
            currentAngle -= 5;

        }

    }


    public void nextLevel ()
    {

        //close the end level panel

        EndLevelCanvas.SetActive(false);

        //load the next level

        int next = currentLevel + 1;
        ChangeLevel(next);

    }

    public void UpdateLevelLocks ()
    {
        //activate all locks

        foreach (GameObject go in Locks)
        {

            go.SetActive(true);

        }

        //deactive allowed locks

        for(int i = 0; i <= (PlayerPrefs.GetInt("HighestLevel")); i++)
        {

            Locks[i].SetActive(false);

        }

    }


    public void endLevel ()
    {
        if (isInPlay)
        {

            //End the level

            Debug.Log("This is where You would end the level");

            //stop isInPlay

            isInPlay = false;

            //load generic menus

            gameCanvas.SetActive(false);
            menuCanvas.SetActive(false);
            hideOnPlay.SetActive(false);
            PauseCanvas.SetActive(false);
            levelSelectionMenu.SetActive(false);
            EndLevelCanvas.SetActive(true);

            //check highest level finished

            if (currentLevel > PlayerPrefs.GetInt("HighestLevel"))
            {

                PlayerPrefs.SetInt("HighestLevel", (currentLevel));

            }
            UpdateLevelLocks();

        }
        

    }

    //toppings

    public void addTopping (string topping)
    {

        Debug.Log(topping);

    }


    //launch the dog

    public void launchDog()
    {
        //remove arrow

        hotDogArrow.SetActive(false);
        hideOnPlay.SetActive(false);

        //add rigidbody to hotdog and launch

        Rigidbody dogRigidbody = hotDogObject.AddComponent<Rigidbody>() as Rigidbody;
        dogRigidbody.angularDrag = 0.01f;
        dogRigidbody.mass = 0.1f;
        Vector3 force = new Vector3(0.1f, -1, 0);
        force = force * launchVelocity;
        dogRigidbody.AddRelativeForce(force);
        isInPlay = true;
        handAnim.CrossFadeInFixedTime("swueeze", 0.1f);

    }

}