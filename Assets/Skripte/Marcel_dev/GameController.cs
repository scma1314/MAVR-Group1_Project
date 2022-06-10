using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private enum GameSteps
    {
        Idle = 0,
        WaitingForStart = 1,
        GetBox = 2,
        UnfoldBox = 3,
        PickObjects = 4,
        FoldBox = 5,
        Confirm = 6,
        End = 7,
        Stop = 8,
        Error = 9            
    }

    private enum FoldSteps
    {
        Idle = 0,
        FBS1 = 1,
        FBS2 = 2,
        FBS3 = 3,
        FBS4 = 4,
        FBS5 = 5,
        FBS6 = 6,
        FBS7 = 7,
        Stop = 8,
        Error = 9
    }

    private enum PickSteps_small
    {
        Idle = 0,
        GearboxBottom = 1,        
        SungearShaft = 2,
        Spacer12 = 3,
        GearboxTop = 4,
        PlanetgearShaft = 5,
        Spacer23 = 6,
        Sungear = 7,
        Planetgear = 8,
        Stop = 9,
        Error = 10        
    }
    
    private enum PickSteps_large
    {
        Idle = 0,
        PlanetgearShaft1 = 1,
        PlanetgearShaft2 = 2,
        PlanetgearShaft3 = 3,
        SungearShaft = 4,
        Planetgear1 = 5,
        Planetgear2 = 6,
        Planetgear3 = 7,
        Sungear = 8,
        Spacer12 = 9,
        Ringgear = 10,
        Spacer23 = 11,
        Planetgear = 12,
        Stop = 13,
        Error = 14
    }


    private GameSteps currentGameStep;
    private FoldSteps currentFoldStep;
    private PickSteps_small currentPickStep_small;
    private PickSteps_large currentPickStep_large;

    private GameSettings settings;
    
    // Start is called before the first frame update
    void Start()
    {
        currentGameStep = new GameSteps();
        currentFoldStep = new FoldSteps();
        currentPickStep_small = new PickSteps_small();
        currentPickStep_large = new PickSteps_large();
        
    }

    // Update is called once per frame
    void Update()
    {
        Game();
    }





    private void Game()
    {
        switch (currentGameStep)
        {
            case GameSteps.Idle:
                break;
            case GameSteps.WaitingForStart:
                break;
            case GameSteps.GetBox:
                break;
            case GameSteps.UnfoldBox:
                Fold();
                break;
            case GameSteps.PickObjects:
                
                break;
            case GameSteps.FoldBox:
                break;
            case GameSteps.Confirm:
                break;
            case GameSteps.End:
                break;
            case GameSteps.Stop:
                break;
            case GameSteps.Error:
                break;
            default:
                break;
        }
    }

    private void Fold()
    {
        switch (currentFoldStep)
        {
            case FoldSteps.Idle:
                break;
            case FoldSteps.FBS1:
                break;
            case FoldSteps.FBS2:
                break;
            case FoldSteps.FBS3:
                break;
            case FoldSteps.FBS4:
                break;
            case FoldSteps.FBS5:
                break;
            case FoldSteps.FBS6:
                break;
            case FoldSteps.FBS7:
                break;
            case FoldSteps.Stop:
                break;
            case FoldSteps.Error:
                break;
            default:
                break;
        }       
    }

    private void Pick_small()
    {
        switch (currentPickStep_small)
        {
            case PickSteps_small.Idle:
                break;
            case PickSteps_small.GearboxBottom:
                break;
            case PickSteps_small.SungearShaft:
                break;
            case PickSteps_small.Spacer12:
                break;
            case PickSteps_small.GearboxTop:
                break;
            case PickSteps_small.PlanetgearShaft:
                break;
            case PickSteps_small.Spacer23:
                break;
            case PickSteps_small.Sungear:
                break;
            case PickSteps_small.Planetgear:
                break;
            case PickSteps_small.Stop:
                break;
            case PickSteps_small.Error:
                break;
            default:
                break;
        }
    }

    private void Pick_large()
    {
        switch (currentPickStep_large)
        {
            case PickSteps_large.Idle:
                break;
            case PickSteps_large.PlanetgearShaft1:
                break;
            case PickSteps_large.PlanetgearShaft2:
                break;
            case PickSteps_large.PlanetgearShaft3:
                break;
            case PickSteps_large.SungearShaft:
                break;
            case PickSteps_large.Planetgear1:
                break;
            case PickSteps_large.Planetgear2:
                break;
            case PickSteps_large.Planetgear3:
                break;
            case PickSteps_large.Sungear:
                break;
            case PickSteps_large.Spacer12:
                break;
            case PickSteps_large.Ringgear:
                break;
            case PickSteps_large.Spacer23:
                break;
            case PickSteps_large.Planetgear:
                break;
            case PickSteps_large.Stop:
                break;
            case PickSteps_large.Error:
                break;
            default:
                break;
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
