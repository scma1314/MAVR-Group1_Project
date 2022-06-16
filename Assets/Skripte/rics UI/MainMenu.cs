using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    private GameSettings settings;
    private GameController controller;

    public void Leicht()
    {
        B_leicht.gameObject.SetActive(false);
        B_schwer.gameObject.SetActive(false);

        settings.HardMode = false;
        settings.SmallBox = false;

        controller.box_animationChild.GameSettings = settings;
        controller.Settings = settings;

        controller.RunGame = true;
        Clock.Timer_running = true;
    }
    public void Schwer()
    {
        B_leicht.gameObject.SetActive(false);
        B_schwer.gameObject.SetActive(false);

        settings.HardMode = true;
        settings.SmallBox = false;

        controller.box_animationChild.GameSettings = settings;
        controller.Settings = settings;

        controller.RunGame = true;
        Clock.Timer_running = true;
    }

    private void Awake()
    {
        settings = ScriptableObject.CreateInstance<GameSettings>();
    }

    void Start()
    {
        settings.HardMode = true;
        settings.SmallBox = false;
        controller = gameObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
