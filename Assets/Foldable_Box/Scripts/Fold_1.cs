using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fold_1 : MonoBehaviour
{
    private Slider mSlider;
    public GameObject Box;

    // Start is called before the first frame update
    void Start()
    {
       mSlider = GetComponent<Slider>(); 
    }

    // Update is called once per frame
    public void ApplyAnimation()
    {
        if(Box != null)
        {   
            Animator animator = Box.GetComponent<Animator>();
            if(animator)
            {
                animator.SetFloat("folding_value",mSlider.value);
            }
            
        }
    }
}
