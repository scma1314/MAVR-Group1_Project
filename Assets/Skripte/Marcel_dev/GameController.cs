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
    private List<GameObject> addedObjects;
    
    private GameSettings settings;

    private bool picking_firstEnter;
    private bool animation_firstEnter;
    private bool runGame;
    private bool debug;

    private Color objColor;
    private Color sZColor;
    private GameObject sZ;
    private GameObject pickObj;

    private MeshRenderer[] boxSZMesh;
    private MeshRenderer[] boxGrabMesh;

    private AnimationController aniController;
    private Timer clock;

    // Public instances
    public GameObject box_small;
    public GameObject box_large;
    public GameObject objectsShelf;
    public GameObject spacerShelf;
    public GameObject assemblyTable;
    public GameObject box_animation;
    public animatio_player box_animationChild;
    public GameObject endBox_small;
    public GameObject endBox_large;
    public GameObject SZEndbox_small;
    public GameObject SZEndbox_large;
    public Clipboard clipboard;


    // Start is called before the first frame update
    void Start()
    {
        box_animationChild = box_animation.GetComponentInChildren<animatio_player>();

        currentGameStep = new GameSteps();
        currentFoldStep = new FoldSteps();
        currentPickStep_small = new PickSteps_small();
        currentPickStep_large = new PickSteps_large();


        //currentPickStep_large = PickSteps_large.Idle;

        addedObjects = new List<GameObject>();
        clock = this.GetComponent<MainMenu>().Clock;

        picking_firstEnter = true;
        animation_firstEnter = true;
        runGame = false;

        debug = false; 

        objColor = Color.black;
        sZColor = Color.black;
        sZ = null;
        pickObj = null;

        //box_animation.GetComponentInChildren<animatio_player>().GameSettings = settings;

        box_small_sz = GetBoxSZ(box_small);
        box_large_sz = GetBoxSZ(box_large);
        InitializeBox(box_small, false, false);
        InitializeBox(box_large, false, false);
        endBox_small.SetActive(false);
        endBox_large.SetActive(false);

        SZEndbox_large.SetActive(false);
        SZEndbox_small.SetActive(false);

        spacerShelf.transform.Find("Spacer").Find("Spacer_hori_505030 (2)").gameObject.SetActive(false);
        spacerShelf.transform.Find("Spacer").Find("Spacer_hori_503040 (2)").gameObject.SetActive(false);
        // SZEndbox_small.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (runGame)
        {            
            Game();
            if (debug) { Debug.Log("Settings boxsize state: " + settings.SmallBox.ToString()); };
        }        
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

                if (picking_firstEnter)
                {
                    sZ = assemblyTable.GetComponentInChildren<XRSocketInteractor>().gameObject;
                    pickObj = box_animation;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponentInChildren<MeshRenderer>().material.color;                    
                    picking_firstEnter = false;

                    boxSZMesh = sZ.GetComponentsInChildren<MeshRenderer>();
                    boxGrabMesh = pickObj.GetComponentsInChildren<MeshRenderer>();                    

                }


                // highlight the snapping zone or the object, depending if the Object is grabbed or not
                // snapping successful
                if ((sZ.GetComponent<XRSocketInteractor>().hasSelection))
                {
                    if (debug) { Debug.Log("snapping of animation Box succesfull"); };

                    //pickObj.GetComponent<MeshRenderer>().material.color = objColor;
                    //sZ.GetComponentInChildren<MeshRenderer>().material.color = sZColor;

                    // deactivate grabbing layers, that object cant be grabbed anymore
                    pickObj.GetComponent<XRGrabInteractable>().interactionLayers = sZ.GetComponent<XRSocketInteractor>().interactionLayers;
                    //pickObj.transform.Find("Grab").gameObject.SetActive(false);

                    foreach (MeshRenderer meshRen in boxGrabMesh)
                    {
                        meshRen.material.color = objColor;
                    }

                    
                    foreach (MeshRenderer meshRen in boxSZMesh)
                    {
                        meshRen.enabled = false;
                    }
                    
                    //pickObj.GetComponent<Rigidbody>().isKinematic = false;
                    // make socketinteractor invisible
                    //sZ.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
                    currentGameStep = GameSteps.UnfoldBox;
                }                
                else if ((pickObj.GetComponent<XRGrabInteractable>().isSelected) && (!Settings.HardMode))
                {
                    if (debug) { Debug.Log("waiting for object to be placed"); };
                    // highlight the snappingzone
                    //pickObj.GetComponentInChildren<MeshRenderer>().material.color = objColor;

                    foreach (MeshRenderer meshRen in boxSZMesh)
                    {
                        meshRen.material.color = Color.green;
                    }

                    foreach (MeshRenderer meshRen in boxGrabMesh)
                    {
                        meshRen.material.color = objColor;
                    }


                }
                else if (!Settings.HardMode)
                {
                    if (debug) { Debug.Log("waiting for object to be grabbed"); };
                    // highlight the Object
                    //sZ.GetComponentInChildren<MeshRenderer>().material.color = sZColor;

                    foreach (MeshRenderer meshRen in boxSZMesh)
                    {
                        meshRen.material.color = sZColor;
                    }

                    foreach (MeshRenderer meshRen in boxGrabMesh)
                    {
                        meshRen.material.color = Color.green;
                    }


                    pickObj.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
                }

                
                break;

            case GameSteps.UnfoldBox:
                //if (debug) { Debug.Log("calling Fold"); };
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
                    //if (debug) { Debug.Log("executing Pick_large"); }
                    if (Pick_large())
                    {
                        currentGameStep = GameSteps.FoldBox;
                    }
                }
                break;

            case GameSteps.FoldBox:

                
                // disable filled Box
                if (settings.SmallBox)
                {
                    InitializeBox(box_small, false, false);                    
                }
                else
                {
                    InitializeBox(box_large, false, false);                    
                }
                
                
                foreach (GameObject gObject in addedObjects)
                {
                    gObject.GetComponent<Rigidbody>().useGravity = false;
                    gObject.GetComponent<Rigidbody>().isKinematic = true;                

                }

                if (animation_firstEnter)
                {
                    if (!settings.HardMode)
                    {
                        aniController.animatio_Player.coll9.GetComponent<MeshRenderer>().material.color = Color.green;
                    }
                    
                    aniController.RestartAnimation(aniController.animatio_Player.coll9);
                    animation_firstEnter = false;  
                }

                if (aniController.AnimationFinished)
                {
                    foreach (GameObject gObject in addedObjects)
                    {
                        gObject.SetActive(false);
                    }

                    box_animation.SetActive(false);

                    if (settings.SmallBox)
                    {
                        endBox_small.SetActive(true);
                    }
                    else
                    {
                        endBox_large.SetActive(true);
                    }

                    currentGameStep = GameSteps.Confirm;
                }
                break;

            case GameSteps.Confirm:
                
                if (settings.SmallBox)
                {
                    if (picking_firstEnter)
                    {
                        SZEndbox_small.SetActive(true);
                        sZColor = SZEndbox_small.GetComponentInChildren<MeshRenderer>().material.color;
                        objColor = endBox_small.GetComponent<MeshRenderer>().material.color;
                        picking_firstEnter = false;

                    }

                    if (PickObject(endBox_small, SZEndbox_small, objColor, sZColor))
                    {
                        currentGameStep = GameSteps.End;
                        picking_firstEnter = true;
                    }
                }
                else
                {
                    if (picking_firstEnter)
                    {
                        SZEndbox_large.SetActive(true);
                        sZColor = SZEndbox_large.GetComponentInChildren<MeshRenderer>().material.color;
                        objColor = endBox_large.GetComponent<MeshRenderer>().material.color;
                        picking_firstEnter = false;
                        
                    }

                    if (PickObject(endBox_large, SZEndbox_large, objColor, sZColor))
                    {
                        currentGameStep = GameSteps.End;
                        picking_firstEnter = true;
                    }
                }
                

                break;

            case GameSteps.End:

                clock.Timer_running = false;
                clipboard.Highscore();
                currentGameStep = GameSteps.Stop;
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
        aniController = box.GetComponentInChildren<AnimationController>();
        

        if (!aniController.GetAnimationRunning)
        {
            if (debug) { Debug.Log("animation restarted"); };
            aniController.RestartAnimation(aniController.animatio_Player.coll1);

        }

        if ((aniController.animatio_Player.coll1.enabled) && (!settings.HardMode))
        {
            aniController.animatio_Player.coll1.GetComponent<MeshRenderer>().material.color = Color.green;
            aniController.animatio_Player.coll6.GetComponent<MeshRenderer>().material.color = Color.green;

        }
        


        if (aniController.AnimationFinished)
        {
            aniController.LockAnimation = true;
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
                currentPickStep_small = PickSteps_small.GearboxBottom;
                // Deactivates box_ani but before animation
                //box_animation.SetActive(false);
                //InitializeBox(box_large, true, false);
                //box_animation.SetActive(false);
                picking_firstEnter = true;

                break;

            case PickSteps_small.GearboxBottom:
                if (picking_firstEnter)
                {
                    sZ = box_small_sz[0].transform.Find("SZ_Getriebegehause").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe2_klein").Find("Getriebegehause (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>(true).material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_small = PickSteps_small.SungearShaft;
                }

                break;

            case PickSteps_small.SungearShaft:
                if (picking_firstEnter)
                {
                    sZ = box_small_sz[0].transform.Find("SZ_Sonnenrad_Welle").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe2_klein").Find("Sonnenrad_Welle (2)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>(true).material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_small = PickSteps_small.Spacer12;
                }

                break;

            case PickSteps_small.Spacer12:
                if (picking_firstEnter)
                {
                    sZ = box_small_sz[3].transform.Find("SZ_Spacer_12").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = spacerShelf.transform.Find("Spacer").Find("Spacer_hori_503040 (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_small = PickSteps_small.GearboxTop;
                }
                break;

            case PickSteps_small.GearboxTop:
                if (picking_firstEnter)
                {
                    sZ = box_small_sz[1].transform.Find("SZ_Getriebedeckel").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe2_klein").Find("Getriebedeckel (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>(true).material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_small = PickSteps_small.PlanetgearShaft;
                }

                break;

            case PickSteps_small.PlanetgearShaft:
                if (picking_firstEnter)
                {
                    sZ = box_small_sz[1].transform.Find("SZ_Planetenrad_Welle").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe2_klein").Find("Planetenrad_Welle (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>(true).material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_small = PickSteps_small.Spacer23;
                }

                break;

            case PickSteps_small.Spacer23:
                if (picking_firstEnter)
                {
                    sZ = box_small_sz[3].transform.Find("SZ_Spacer_23").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = spacerShelf.transform.Find("Spacer").Find("Spacer_hori_503040 (2)").gameObject;
                    pickObj.SetActive(true);
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_small = PickSteps_small.Sungear;
                }
                break;

            case PickSteps_small.Sungear:
                if (picking_firstEnter)
                {
                    sZ = box_small_sz[2].transform.Find("SZ_Sonnenrad").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe2_klein").Find("Sonnenrad (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>(true).material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_small = PickSteps_small.Planetgear;
                }

                break;

            case PickSteps_small.Planetgear:
                if (picking_firstEnter)
                {
                    sZ = box_small_sz[2].transform.Find("SZ_Planetenrad").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe2_klein").Find("Planetenrad (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>(true).material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_small = PickSteps_small.Stop;
                }

                break;
            case PickSteps_small.Stop:
                return true;
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
        //if (debug) { Debug.Log("executing Pick_large in step: " + currentPickStep_large); };

        switch (currentPickStep_large)
        {
            case PickSteps_large.Idle:
                currentPickStep_large = PickSteps_large.PlanetgearShaft1;
                // Deactivates box_ani but before animation
                //box_animation.SetActive(false);
                //InitializeBox(box_large, true, false);
                //box_animation.SetActive(false);
                picking_firstEnter = true;
                break;

            case PickSteps_large.PlanetgearShaft1:
                 
                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad_Welle").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad_Welle (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>(true).material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.PlanetgearShaft2;
                }
                
                break;

            case PickSteps_large.PlanetgearShaft2:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad_Welle (1)").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad_Welle (2)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.PlanetgearShaft3;
                }

                break;

            case PickSteps_large.PlanetgearShaft3:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad_Welle (2)").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad_Welle (3)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.SungearShaft;
                }

                break;

            case PickSteps_large.SungearShaft:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Sonnenrad_Welle").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Sonnenrad_Welle (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Planetgear1;
                }

                break;

            case PickSteps_large.Planetgear1:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Planetgear2;
                }

                break;

            case PickSteps_large.Planetgear2:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad (1)").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad (2)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Planetgear3;
                }

                break;

            case PickSteps_large.Planetgear3:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Planetenrad (2)").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad (3)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Sungear;
                }

                break;

            case PickSteps_large.Sungear:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[0].transform.Find("SZ_Sonnenrad").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Sonnenrad (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Spacer12;
                }

                break;

            case PickSteps_large.Spacer12:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[3].transform.Find("SZ_Spacer_12").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = spacerShelf.transform.Find("Spacer").Find("Spacer_hori_505030 (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Ringgear;
                }

                break;

            case PickSteps_large.Ringgear:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[1].transform.Find("SZ_Hohlrad").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Hohlrad (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Spacer23;
                }

                break;

            case PickSteps_large.Spacer23:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[3].transform.Find("SZ_Spacer_23").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = spacerShelf.transform.Find("Spacer").Find("Spacer_hori_505030 (2)").gameObject;
                    pickObj.SetActive(true);
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
                {
                    picking_firstEnter = true;
                    currentPickStep_large = PickSteps_large.Planetgear;
                }

                break;

            case PickSteps_large.Planetgear:

                if (picking_firstEnter)
                {
                    sZ = box_large_sz[2].transform.Find("SZ_Planetenrad_Traeger").gameObject;
                    ReactivateSZ(sZ);
                    pickObj = objectsShelf.transform.Find("Pick_Objects").Find("Aufgabe1_groﬂ").Find("Planetenrad_Traeger (1)").gameObject;
                    sZColor = sZ.GetComponentInChildren<MeshRenderer>().material.color;
                    objColor = pickObj.GetComponent<MeshRenderer>().material.color;
                    addedObjects.Add(pickObj);
                    picking_firstEnter = false;
                    if (debug) { Debug.Log("executed firstenter and got " + pickObj.name); };
                }

                if ((PickObject(pickObj, sZ, objColor, sZColor)) && (!picking_firstEnter))
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
        if (debug) { Debug.Log("executing PickObject Method"); };

        // highlight the snapping zone or the object, depending if the Object is grabbed or not
        if (snappingZone.GetComponent<XRSocketInteractor>().hasSelection)
        {
                         
            pickObject.GetComponent<MeshRenderer>().material.color = objectColor;
            //snappingZone.GetComponentInChildren<MeshRenderer>(true).material.color = snappingZoneColor;

            // deactivate snapping zone mesh rendering
            // snappingZone.gameObject.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);

            // deactivate all layers from the Object so it cant be picked again
            pickObject.GetComponent<XRGrabInteractable>().interactionLayers = snappingZone.GetComponent<XRSocketInteractor>().interactionLayers;

            return true;
        }
        else if ((pickObject.GetComponent<XRGrabInteractable>().isSelected) && (!Settings.HardMode))
        {
            // dehilight the object
            pickObject.GetComponent<MeshRenderer>().material.color = objectColor;

            // highlight the snappingzone
            snappingZone.GetComponentInChildren<MeshRenderer>(true).material.color = Color.green;
            
        }
        else if (!Settings.HardMode)
        {
            //if (debug) { Debug.Log("executing not hardmode"); };
            // highlight the Object
            pickObject.GetComponent<MeshRenderer>().material.color = Color.green;

            // dehighlight the snappingzone
            snappingZone.GetComponentInChildren<MeshRenderer>(true).material.color = snappingZoneColor;
        }
        return false;
    }
    
    private void InitializeBox(GameObject box, bool boxActive = true, bool snapZonesActive = true)
    {
        GameObject boxParts;
        GameObject boxExternParts;
        GameObject snapZones;
        Component[] boxComponents;
        Component[] boxExternComponents;
        Component[] snapComponents;

        boxParts = box.transform.Find("Box_parts").gameObject;
        boxExternParts = box.transform.Find("Side_Parts").gameObject;

        boxComponents = boxParts.GetComponentsInChildren<MeshRenderer>(true);
        boxExternComponents = boxExternParts.GetComponentsInChildren<MeshRenderer>(true);

        snapZones = box.transform.Find("SnapZones").gameObject;
        snapComponents = snapZones.GetComponentsInChildren<XRSocketInteractor>(true);

        if (!boxActive)
        {                        
            foreach (MeshRenderer mesh in boxComponents)
            {
                mesh.enabled = false;
                mesh.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            foreach (MeshRenderer mesh in boxExternComponents)
            {
                mesh.enabled = false;
                mesh.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        else
        {
            foreach (MeshRenderer mesh in boxComponents)
            {
                mesh.enabled = true;
                mesh.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            foreach (MeshRenderer mesh in boxExternComponents)
            {
                mesh.enabled = true;
                mesh.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }

        if (!snapZonesActive)
        {
            foreach (XRSocketInteractor comp in snapComponents)
            {
                comp.transform.gameObject.GetComponentInChildren<MeshRenderer>(true).enabled = false;
                comp.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                comp.enabled = false;
            }
        }
        else
        {
            foreach (XRSocketInteractor comp in snapComponents)
            {
                comp.transform.gameObject.GetComponentInChildren<MeshRenderer>(true).enabled = true;
                comp.transform.gameObject.GetComponent<BoxCollider>().enabled = true;                
                comp.enabled = true;
            }
        }
    }


    private void ReactivateSZ(GameObject sZ)
    {
        sZ.transform.gameObject.GetComponentInChildren<MeshRenderer>(true).enabled = true;
        sZ.transform.gameObject.GetComponent<BoxCollider>().enabled = true;
        sZ.GetComponent<XRSocketInteractor>().enabled = true;
        //sZ.SetActive(true);
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
        outList.Add(box.transform.Find("SnapZones").gameObject.transform.Find("Level_1").gameObject);
        outList.Add(box.transform.Find("SnapZones").gameObject.transform.Find("Level_2").gameObject);
        outList.Add(box.transform.Find("SnapZones").gameObject.transform.Find("Level_3").gameObject);
        outList.Add(box.transform.Find("SnapZones").gameObject.transform.Find("Spacer").gameObject);

       
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

    public bool RunGame
    {
        get { return runGame; }
        set { runGame = value; }
    }
}
