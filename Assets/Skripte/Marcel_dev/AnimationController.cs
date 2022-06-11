using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public animatio_player animatio_Player;

    /// <summary>
    /// switch debug printouts On/Off
    /// </summary>
    private bool debug;

    private GameSettings settings;

    
    private bool lockAnimation;
    

    // Start is called before the first frame update
    void Start()
    {
        debug = true;
        
        lockAnimation = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.realtimeSinceStartup >= 30.0) && (LockAnimation))
        {
            LockAnimation = false;
            if (debug) { Debug.Log("Animation unlocked after: " + Time.realtimeSinceStartup.ToString() + " seconds"); }
        }

    }

    public void SetAnimationState(string nextState)
    {
        if (!LockAnimation)
        {
            animatio_Player.SetState(nextState);
        }
        else
        {
            if (debug) { Debug.Log("Tried to move to state: " + nextState + " but animation is locked"); }
        }
        
    }





    public bool LockAnimation
    {
        get
        {
            return lockAnimation;
        }
        set
        {
            lockAnimation = value;
        }
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
