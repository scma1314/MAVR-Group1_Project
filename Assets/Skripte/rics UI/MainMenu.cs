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

    public void Leicht()
    {
        B_leicht.gameObject.SetActive(false);
        B_schwer.gameObject.SetActive(false);
    }
    public void Schwer()
    {
        B_leicht.gameObject.SetActive(false);
        B_schwer.gameObject.SetActive(false);
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
