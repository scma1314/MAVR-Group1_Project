using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bla : MonoBehaviour
{
    private HEYho hey;

    // Start is called before the first frame update
    void Start()
    {
        hey = FindObjectOfType<HEYho>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
      
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
