using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animatio_player : MonoBehaviour
{

    string currentstate;

    //define diffrent states  
    //TODO replace with enum 
    const string IDLE = "IDLE";
    const string FBS1 = "folding_box_step_1";
    const string FBS2 = "folding_box_step_2";
    const string FBS3 = "folding_box_step_3";
    const string FBS4 = "folding_box_step_4";
    const string FBS5 = "folding_box_step_5";
    const string FBS6 = "folding_box_step_6";
    const string FBS7 = "folding_box_step_7";


    //instantiate the buttons
    public Button Button_1;
    public Button Button_2;
    public Button Button_3;
    public Button Button_4;
    public Button Button_5;
    public Button Button_6;
    public Button Button_7;

    //State variable for buttons 

    bool Button_1_bool = false;
    bool Button_2_bool = false;
    bool Button_3_bool = false;
    bool Button_4_bool = false;
    bool Button_5_bool = false;
    bool Button_6_bool = false;
    bool Button_7_bool = false;

    //State variable for buttons 
    public Collider coll1;
    public Collider coll2;
    public Collider coll3;
    public Collider coll4;
    public Collider coll5;
    public Collider coll6;
    public Collider coll7;

    //instantiate te animator 
    Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        /*
        Button_1.onClick.AddListener(Button_1_called);
        Button_2.onClick.AddListener(Button_2_called);
        Button_3.onClick.AddListener(Button_3_called);
        Button_4.onClick.AddListener(Button_4_called);
        Button_5.onClick.AddListener(Button_5_called);
        Button_6.onClick.AddListener(Button_6_called);
        Button_7.onClick.AddListener(Button_7_called);
        */

        myAnimator = GetComponent<Animator>();
        currentstate = "";
        //deactivate all button at start except button 1 
        /*
        Button_2.gameObject.SetActive(false);
        Button_3.gameObject.SetActive(false);
        Button_4.gameObject.SetActive(false);
        Button_5.gameObject.SetActive(false);
        Button_6.gameObject.SetActive(false);
        Button_7.gameObject.SetActive(false);
        */

        coll1.enabled = true;
        coll2.enabled = false;
        coll3.enabled = false;
        coll4.enabled = false;
        coll5.enabled = false;
        coll6.enabled = false;
        coll7.enabled = false;
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
            Debug.Log("state: " + currentstate.ToString());

            myAnimator.Play(FBS1);
            /*
            Button_1_bool = false;
            Button_2.gameObject.SetActive(true);
            */
            coll1.enabled = false;
            coll2.enabled = true;
            
        }


        if (currentstate == FBS1)// && Button_2_bool == true)
        {
            Debug.Log("state: " + currentstate.ToString());
            myAnimator.Play(FBS2);
            /*
            Button_2_bool = false;
            Button_3.gameObject.SetActive(true);
            */
            coll2.enabled = false;
            coll3.enabled = true;

        }

        if (currentstate == FBS2)// && Button_3_bool == true)
        {
            Debug.Log("state: " + currentstate.ToString());
            myAnimator.Play(FBS3);
            /*
            Button_3_bool = false;
            Button_4.gameObject.SetActive(true);
            */
            coll3.enabled = false;
            coll4.enabled = true;

        }

        if (currentstate == FBS3)// && Button_4_bool == true)
        {
            Debug.Log("state: " + currentstate.ToString());
            myAnimator.Play(FBS4);
            /*
            Button_4_bool = false;
            Button_5.gameObject.SetActive(true);
            */
            coll4.enabled = false;
            coll5.enabled = true;

        }

        if (currentstate == FBS4)// && Button_5_bool == true)
        {
            Debug.Log("state: " + currentstate.ToString());
            myAnimator.Play(FBS5);
            /*
            Button_5_bool = false;
            Button_6.gameObject.SetActive(true);
            */
            coll5.enabled = false;
            coll6.enabled = true;

        }

        if (currentstate == FBS5)// && Button_6_bool == true)
        {
            Debug.Log("state: " + currentstate.ToString());
            myAnimator.Play(FBS6);
            /*
            Button_6_bool = false;
            Button_7.gameObject.SetActive(true);
            */
            coll6.enabled = false;
            coll7.enabled = true;

        }

        if (currentstate == FBS6)// && Button_7_bool == true)
        {
            Debug.Log("state: " + currentstate.ToString());
            myAnimator.Play(FBS7);
            /*
            Button_7_bool = false;
            */
            coll7.enabled = false;
            coll1.enabled = true;
        }
    }


    void Button_1_called()
    {

        Button_1_bool = true;
        //hide button afte it is pressed 
        Button_1.gameObject.SetActive(false);

    }

    void Button_2_called()
    {

        Button_2_bool = true;
        //hide button afte it is pressed 
        Button_2.gameObject.SetActive(false);

    }

    void Button_3_called()
    {

        Button_3_bool = true;
        //hide button afte it is pressed 
        Button_3.gameObject.SetActive(false);

    }

    void Button_4_called()
    {

        Button_4_bool = true;
        //hide button afte it is pressed 
        Button_4.gameObject.SetActive(false);

    }

    void Button_5_called()
    {

        Button_5_bool = true;
        //hide button afte it is pressed 
        Button_5.gameObject.SetActive(false);

    }

    void Button_6_called()
    {

        Button_6_bool = true;
        //hide button afte it is pressed 
        Button_6.gameObject.SetActive(false);

    }

    void Button_7_called()
    {

        Button_7_bool = true;
        //hide button afte it is pressed 
        Button_7.gameObject.SetActive(false);

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