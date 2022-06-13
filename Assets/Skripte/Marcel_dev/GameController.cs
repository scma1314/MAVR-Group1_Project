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

    private List<GameObject> box_large_sz;
    private List<GameObject> box_small_sz;
    
    private GameSettings settings;

    private bool picking_firstEnter;

    private Color objColor;
    private Color sZColor;
    private GameObject sZ;
    private GameObject pickObj;

    // Public instances
    public GameObject box_small;
    public GameObject box_large;
    public GameObject objectsShelf;
    public GameObject spacerShelf;
    public GameObject assemblyTable;
    public GameObject box_animation;
    
    

    // Start is called before the first frame update
    void Start()
    {
        currentGameStep = new GameSteps();
        currentFoldStep = new FoldSteps();
        currentPickStep_small = new PickSteps_small();
        currentPickStep_large = new PickSteps_large();

        box_small_sz = GetBoxSZ(box_small);
        box_large_sz = GetBoxSZ(box_large);

        picking_firstEnter = true;

        objColor = Color.black;
        sZColor = Color.black;
        sZ = null;
        pickObj = null;
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

                    assemblyTable.GetComponentInChildren<XRSocketInteractor>().gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;

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
                    assemblyTable.GetComponentInChildren<XRSocketInteractor>().gameObject.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
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
                                
                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad_Welle").gameObject;
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad_Welle (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.PlanetgearShaft2;
                }
                
                break;

            case PickSteps_large.PlanetgearShaft2:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad_Welle (1)").gameObject;
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad_Welle (2)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.PlanetgearShaft3;
                }

                break;

            case PickSteps_large.PlanetgearShaft3:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad_Welle (2)").gameObject;
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad_Welle (3)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.SungearShaft;
                }

                break;

            case PickSteps_large.SungearShaft:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Sonnenrad_Welle").gameObject;
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Sonnenrad_Welle (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Planetgear1;
                }

                break;

            case PickSteps_large.Planetgear1:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad").gameObject;
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Planetgear2;
                }

                break;

            case PickSteps_large.Planetgear2:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad (1)").gameObject;
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad (2)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Planetgear3;
                }

                break;

            case PickSteps_large.Planetgear3:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad (2)").gameObject;
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad (3)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Sungear;
                }

                break;

            case PickSteps_large.Sungear:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Sonnenrad").gameObject;
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Sonnenrad (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Spacer12;
                }

                break;

            case PickSteps_large.Spacer12:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[3].transform.Find("SZ_Spacer_12").gameObject;
                    pickObj = spacerShelf.transform.Find("Spacer").Find("Spacer_hori_505030 (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Ringgear;
                }

                break;

            case PickSteps_large.Ringgear:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[1].transform.Find("SZ_Hohlrad").gameObject;
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Hohlrad (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Spacer23;
                }

                break;

            case PickSteps_large.Spacer23:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[3].transform.Find("SZ_Spacer_23").gameObject;
                    pickObj = spacerShelf.transform.Find("Spacer").Find("Spacer_hori_505030 (2)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Planetgear;
                }

                break;

            case PickSteps_large.Planetgear:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[2].transform.Find("SZ_Planetenrad_Traeger").gameObject;
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad_Traeger (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    picking_firstEnter = false;
                }

                if (PickObject(pickObj, sZ, objColor, sZColor))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Stop;
                }

                break;

            case PickSteps_large.Stop:
                return true;
                break;

            case PickSteps_large.Error:
                break;

            default:
                break;
        }

        return false;
    }

    private bool PickObject(GameObject pickObject, GameObject snappingZone, Color objectColor, Color snappingZoneColor)
    {
        

        // highlight the snapping zone or the object, depending if the Object is grabbed or not
        if ((pickObject.GetComponent<XRGrabInteractable>().isSelected) && (!Settings.HardMode))
        {
            // dehilight the object
            pickObject.GetComponent<MeshRenderer>().material.color = objectColor;

            // highlight the snappingzone
            snappingZone.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
            
        }
        else if (!Settings.HardMode)
        {
            // highlight the Object
            pickObject.GetComponent<MeshRenderer>().material.color = Color.green;

            // dehighlight the snappingzone
            snappingZone.GetComponentInChildren<MeshRenderer>().material.color = snappingZoneColor;
        }


        // deactivate object and snapping zone when the object has entered it. so it can't be picked again
        if (snappingZone.GetComponent<XRSocketInteractor>().hasSelection)
        {
            pickObject.GetComponent<MeshRenderer>().material.color = objectColor;
            snappingZone.GetComponentInChildren<MeshRenderer>().material.color = snappingZoneColor;
            
            // deactivate snapping zone mesh rendering
            // snappingZone.gameObject.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);

            // deactivate all layers from the Object so it cant be picked again
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


    private List<GameObject> GetBoxSZ(GameObject box)
    {
        List<GameObject> outList = new List<GameObject>();

        /*outList.Add(new List<GameObject>());
        outList.Add(new List<GameObject>());
        outList.Add(new List<GameObject>());
        outList.Add(new List<GameObject>());
        */

        //GameObject sZParent = box.transform.Find("SnapZones").gameObject;
        outList[0] = box.transform.Find("SnapZones").gameObject.transform.Find("Level_1").gameObject;
        outList[1] = box.transform.Find("SnapZones").gameObject.transform.Find("Level_2").gameObject;
        outList[2] = box.transform.Find("SnapZones").gameObject.transform.Find("Level_3").gameObject;
        outList[3] = box.transform.Find("SnapZones").gameObject.transform.Find("Spacer").gameObject;

       
        /*
        foreach (Component component in level1.GetComponentsInChildren<XRSocketInteractor>())
        {
            outList[1].Add(component.gameObject);
        }

        foreach (Component component in level2.GetComponentsInChildren<XRSocketInteractor>())
        {
            outList[2].Add(component.gameObject);
        }

        foreach (Component component in level3.GetComponentsInChildren<XRSocketInteractor>())
        {
            outList[3].Add(component.gameObject);
        }

        foreach (Component component in spacer.GetComponentsInChildren<XRSocketInteractor>())
        {
            outList[4].Add(component.gameObject);
        }
        */

        return outList;
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
