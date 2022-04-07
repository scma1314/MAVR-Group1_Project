using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{

    public GameObject ball;
    public GameObject cylinder;
    public GameObject jodl;

    private ConstantForce constForce;

    // Start is called before the first frame update
    void Start()
    {
        ball = this.gameObject;
        cylinder = this.gameObject;

        constForce = cylinder.GetComponent<ConstantForce>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec3;
        vec3 = new Vector3(3, 3, 3);
        //constForce.force = vec3;

        constForce.force = vec3;



    }



    private void DelayedStart(GameObject obj)
    {
        

    }
}
