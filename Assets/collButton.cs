using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class collButton : MonoBehaviour
{
    public AnimationController animationController;
    public string state;


    private void FixedUpdate()
    {
        if (animationController == null)
            animationController = FindObjectOfType<AnimationController>();
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<XRDirectInteractor>() != null)
            animationController.SetAnimationState(state);
        Debug.Log("calling " + animationController.name + " from " + this.name);
    }
}
