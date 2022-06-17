using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Szenenwechsel : MonoBehaviour

{
    public GameSettings settings;

    public void Scene1()
    {
        SceneManager.LoadScene("CommissioningRoom_Order_Small");
        //settings.SmallBox = true;
    }
    public void Scene2()
    {
        SceneManager.LoadScene("CommissioningRoom_Order_Large");
        //settings.SmallBox = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public GameSettings GameSettings
    {
        get { return settings; }
        set { settings = value; }
    }

}
