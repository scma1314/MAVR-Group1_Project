using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    private bool smallBox;
    private bool hardMode;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // properties for private variables
    public bool SmallBox
    {
        get
        {
            return smallBox;
        }
        set
        {
            smallBox = value;
        }
    }

    public bool HardMode
    {
        get
        {
            return hardMode;
        }
        set
        {
            hardMode = value;
        }
    }
}
