using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    //TODO replace with enum 
    public Button B_leicht;
    public Button B_schwer;
    public Button B_A1;
    public Button B_A2;
    public Button B_reset;
    public Button B_quit;

    public Timer Clock;
    public Szenenwechsel szenenwechsel;

    private GameSettings settings;
    private GameController controller;


    public void Leicht()
    {
        B_leicht.gameObject.SetActive(false);
        B_schwer.gameObject.SetActive(false);

        settings.HardMode = false;

        controller.box_animationChild.GameSettings = settings;
        controller.Settings = settings;
        szenenwechsel.GameSettings = settings;

        controller.RunGame = true;
        Clock.Timer_running = true;
    }
    public void Schwer()
    {
        B_leicht.gameObject.SetActive(false);
        B_schwer.gameObject.SetActive(false);

        settings.HardMode = true;

        controller.box_animationChild.GameSettings = settings;
        controller.Settings = settings;
        szenenwechsel.GameSettings = settings;

        controller.RunGame = true;
        Clock.Timer_running = true;
    }

    private void Awake()
    {
        settings = ScriptableObject.CreateInstance<GameSettings>();
    }

    void Start()
    {
        settings.HardMode = false;
        if (SceneManager.GetActiveScene().name == "CommissioningRoom_Order_Small")
        {
            settings.SmallBox = true;
        }
        else if (SceneManager.GetActiveScene().name == "CommissioningRoom_Order_Large")
        {
            settings.SmallBox = false;
        }
        else
        {
            Debug.LogWarning("Unknown Scene detected");
        }
        
        controller = gameObject.GetComponent<GameController>();
        szenenwechsel.GameSettings = settings;
    }

    // Update is called once per frame
    void Update()
    {
        //szenenwechsel.GameSettings = settings;
    }
}
