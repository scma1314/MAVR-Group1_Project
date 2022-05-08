using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fold_cardboard_box : MonoBehaviour
{
    private Slider mSlider; 

    public GameObject Box;


    // Start is called before the first frame update
    void Start()
    {
        mSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    public void applayAnimation()
    {
        if(Box !=null )
        {
            Animator animator = Box.GetComponent<Animator>();
        

            if(animator)
            {
                animator.SetFloat("fold_value",mSlider.value);
            }
        }
    }
}
