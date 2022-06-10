using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    /// <summary>
    /// The box this Controller runs on
    /// </summary>
    public GameObject box;

    /// <summary>
    /// Information if it is a small Box or a large Box
    /// </summary>
    public bool smallBox;


    private GameSettings settings;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public GameSettings Settings
    {
        get
        {
            return settings;
        }
        set
        {
            settings = value;
        }
    }
}
