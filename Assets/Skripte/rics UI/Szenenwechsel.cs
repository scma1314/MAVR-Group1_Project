using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Szenenwechsel : MonoBehaviour

{
    public void Scene1()
    {
        SceneManager.LoadScene("Laboratory_Room_Scene_small_");
    }
    public void Scene2()
    {
        SceneManager.LoadScene("Laboratory_Room _Leicht2_");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
