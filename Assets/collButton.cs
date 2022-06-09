using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class collButton : MonoBehaviour
{
    public animatio_player ani;
    public string state;
    private void Start()
    {
        ani = FindObjectOfType<animatio_player>();
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<XRDirectInteractor>() != null)
            ani.SetState(state);
        Debug.Log("calling " + ani.name + " from " + this.name);
    }
}
