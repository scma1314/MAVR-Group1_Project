using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/*
 * ToDo:
 * reset highlighted colors 
 * check if objects fall when deactivating the socketinteractor
 * 
 */
    

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

    // Public instances
    public GameObject box_small;
    public GameObject box_large;
    public GameObject objectsShelf;
    public GameObject assemblyTable;
    public GameObject box_animation;
    
    

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
                currentGameStep = GameSteps.WaitingForStart;
                break;

            case GameSteps.WaitingForStart:
                // waiting for user to press Start button
                currentGameStep = GameSteps.GetBox;
                break;

            case GameSteps.GetBox:

                // highlight the snapping zone or the object, depending if the Object is grabbed or not
                if ((box_animation.GetComponent<XRGrabInteractable>().isSelected) && (!Settings.HardMode))
                {
                    // highlight the snappingzone

                    assemblyTable.GetComponent<XRSocketInteractor>().gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;

                }
                else if (!Settings.HardMode)
                {
                    // highlight the Object
                    box_animation.GetComponent<MeshRenderer>().material.color = Color.green;
                }

                // snapping successful
                if ((assemblyTable.GetComponent<XRSocketInteractor>().hasSelection))
                {
                    // deactivate grabbing layers, that object cant be grabbed anymore
                    box_animation.GetComponent<XRGrabInteractable>().interactionLayers = 0;

                    // make socketinteractor invisible
                    assemblyTable.GetComponent<XRSocketInteractor>().gameObject.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
                    currentGameStep = GameSteps.UnfoldBox;
                }

                break;

            case GameSteps.UnfoldBox:
                if (!Fold(box_animation))
                {

                }
                else
                {
                    currentGameStep = GameSteps.PickObjects;
                };
                
                break;

            case GameSteps.PickObjects:
                if (settings.SmallBox)
                {
                    if(Pick_small())
                    {
                        currentGameStep = GameSteps.FoldBox;
                    }
                }
                else
                {
                    if (Pick_large())
                    {
                        currentGameStep = GameSteps.FoldBox;
                    }
                }
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

    private bool Fold(GameObject box)
    {   
        AnimationController aniController = box.GetComponentInChildren<AnimationController>();

        if (!aniController.GetAnimationRunning)
        {
            aniController.RestartAnimation();
        }
        

        if (aniController.AnimationFinished)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool Pick_small()
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

        return false;
    }

    private bool Pick_large()
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

        return false;
    }

    private bool PickObject(GameObject pickObject, GameObject snappingZone)
    {
        
        // highlight the snapping zone or the object, depending if the Object is grabbed or not
        if ((pickObject.GetComponent<XRGrabInteractable>().isSelected) && (!Settings.HardMode))
        {
            // highlight the snappingzone
            snappingZone.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
            
        }
        else if (!Settings.HardMode)
        {
            // highlight the Object
            pickObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }


        // deactivate object and snapping zone when the object has entered it. so it can't be picked again
        if (snappingZone.GetComponent<XRSocketInteractor>().hasSelection)
        {
            // deactivate snapping zone mesh rendering
            snappingZone.gameObject.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);

            // deactivate all all layers from the Object so it cant be picked again
            pickObject.GetComponent<XRGrabInteractable>().interactionLayers = 0;

            return true;
        }

        return false;
    }
    
    private void InitializeBox(GameObject box, bool boxActive = true, bool snapZonesActive = true)
    {
        GameObject boxParts;
        GameObject snapZones;
        Component[] boxComponents;
        Component[] snapComponents;

        if (!boxActive)
        {
            boxParts = box.transform.Find("Box_parts").gameObject;
            boxComponents = boxParts.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer mesh in boxComponents)
            {
                mesh.enabled = false;
                mesh.gameObject.GetComponent<BoxCollider>().enabled = false;
            }            
        }

        if (!snapZonesActive)
        {
            snapZones = box.transform.Find("Snapzones").gameObject;
            snapComponents = snapZones.GetComponentsInChildren<XRSocketInteractor>();

            foreach (XRSocketInteractor comp in snapComponents)
            {
                comp.transform.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                comp.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                comp.enabled = false;
            }
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
