using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animatio_player : MonoBehaviour
{

    private bool debug;
    private string currentstate;
    private bool animationFinished;

    //define diffrent states  
    //TODO replace with enum 
    const string IDLE = "IDLE";
    const string FBS0 = "folding_box_step_0_0";
    const string FBS1 = "folding_box_step_0_1";
    const string FBS2 = "folding_box_step_2";
    const string FBS3 = "folding_box_step_3";
    const string FBS4 = "folding_box_step_4";
    const string FBS5 = "folding_box_step_5";
    const string FBS6 = "folding_box_step_6";
    const string FBS7 = "folding_box_step_8";


    //State variable for buttons 
    public Collider coll1;
    public Collider coll2;
    public Collider coll3;
    public Collider coll4;
    public Collider coll5;
    public Collider coll6;
    public Collider coll7;
    public Collider coll8;

    private Color coll1_basecolor;
    private Color coll2_basecolor;
    private Color coll3_basecolor;
    private Color coll4_basecolor;
    private Color coll5_basecolor;
    private Color coll6_basecolor;
    private Color coll7_basecolor;
    private Color coll8_basecolor;
    private Color coll9_basecolor;




    //instantiate te animator 
    Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        currentstate = "";
        AnimationFinished = false;
        
        debug = true;

        coll1.enabled = true; // switch to false later
        coll2.enabled = false;
        coll3.enabled = false;
        coll4.enabled = false;
        coll5.enabled = false;
        coll6.enabled = false;
        coll7.enabled = false;
        coll8.enabled = false;
        coll1_basecolor = coll1.GetComponent<MeshRenderer>().material.color;
        coll2_basecolor = coll2.GetComponent<MeshRenderer>().material.color;
        coll3_basecolor = coll3.GetComponent<MeshRenderer>().material.color;
        coll4_basecolor = coll4.GetComponent<MeshRenderer>().material.color;
        coll5_basecolor = coll5.GetComponent<MeshRenderer>().material.color;
        coll6_basecolor = coll6.GetComponent<MeshRenderer>().material.color;
        coll7_basecolor = coll7.GetComponent<MeshRenderer>().material.color;
        coll8_basecolor = coll8.GetComponent<MeshRenderer>().material.color;

        coll1.GetComponent<MeshRenderer>().material.color = Color.green;
        //coll9_basecolor = coll9.GetComponent<MeshRenderer>().material.color;

    }

    public void SetState(string nextstate)
    {
        currentstate = nextstate;
        Debug.Log("called with State: " + nextstate);
    }

    private void Update()
    {
        if (currentstate == IDLE)// && Button_1_bool == true)
        {
            if (debug) { Debug.Log("state: " + currentstate.ToString()); }

            myAnimator.Play(FBS0);


            coll1.GetComponent<MeshRenderer>().material.color = coll1_basecolor;
            coll8.GetComponent<MeshRenderer>().material.color = Color.green;


            coll1.enabled = false;
            coll8.enabled = true;
            
        }

        /*
        if (currentstate == FBS0)// && Button_2_bool == true)
        {
            if (debug) { Debug.Log("state: " + currentstate.ToString()); }
            myAnimator.Play(FBS1);



            coll8.GetComponent<MeshRenderer>().material.color = coll8_basecolor;
            coll2.GetComponent<MeshRenderer>().material.color = Color.green;

            coll8.enabled = false;
            coll2.enabled = true;

        }

        */

        if (currentstate == FBS0)// && Button_2_bool == true)
        {
            if (debug) { Debug.Log("state: " + currentstate.ToString()); }
            myAnimator.Play(FBS2);

            coll8.GetComponent<MeshRenderer>().material.color = coll8_basecolor;
            coll3.GetComponent<MeshRenderer>().material.color = Color.green;

            coll8.enabled = false;
            coll3.enabled = true;

        }

        if (currentstate == FBS2)// && Button_3_bool == true)
        {
            if (debug) { Debug.Log("state: " + currentstate.ToString()); }
            myAnimator.Play(FBS3);

            coll3.GetComponent<MeshRenderer>().material.color = coll3_basecolor;
            coll4.GetComponent<MeshRenderer>().material.color = Color.green;

            coll3.enabled = false;
            coll4.enabled = true;

        }

        if (currentstate == FBS3)// && Button_4_bool == true)
        {
            if (debug) { Debug.Log("state: " + currentstate.ToString()); }
            myAnimator.Play(FBS4);

            coll4.GetComponent<MeshRenderer>().material.color = coll4_basecolor;
            coll5.GetComponent<MeshRenderer>().material.color = Color.green;

            coll4.enabled = false;
            coll5.enabled = true;

        }

        if (currentstate == FBS4)// && Button_5_bool == true)
        {
            if (debug) { Debug.Log("state: " + currentstate.ToString()); }
            myAnimator.Play(FBS5);

            coll5.GetComponent<MeshRenderer>().material.color = coll5_basecolor;
            coll6.GetComponent<MeshRenderer>().material.color = Color.green;

            coll5.enabled = false;
            coll6.enabled = true;

        }

        if (currentstate == FBS5)// && Button_6_bool == true)
        {
            if (debug) { Debug.Log("state: " + currentstate.ToString()); }
            myAnimator.Play(FBS6);

            coll6.GetComponent<MeshRenderer>().material.color = coll6_basecolor;
            coll7.GetComponent<MeshRenderer>().material.color = Color.green;

            coll6.enabled = false;
            coll7.enabled = true;

        }

        if (currentstate == FBS6)// && Button_7_bool == true)
        {
            if (debug) { Debug.Log("state: " + currentstate.ToString()); }
            myAnimator.Play(FBS7);

            AnimationFinished = true;

            coll7.GetComponent<MeshRenderer>().material.color = coll3_basecolor;

            coll7.enabled = false;
            //coll1.enabled = true;
        }
    }

    public void ResetAnimation()
    {

    }


    public bool AnimationFinished
    {
        get
            { return animationFinished; }
        set
            { animationFinished = value; }
    }

    public string GetCurrentstate
    {
        get { return currentstate; }
    }

    // private void Awake() 
    // {
    //    Button_1.onClick.AddListener(OnButtonClick);

    // }

    // private void OnButtonClick()
    // {

    //     if (currentstate == IDLE)
    //     {
    //         myAnimator.Play(FBS1);
    //         currentstate=FBS1;
    //         return;
    //         //Button_1.GetComponentInChildren<Text>().text = "F";
    //     }

    //     if (currentstate == FBS2);
    //     {
    //         myAnimator.Play(FBS2);
    //         currentstate=FBS2;
    //     }



    //}




}