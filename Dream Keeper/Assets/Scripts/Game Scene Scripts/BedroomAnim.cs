using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomAnim : MonoBehaviour
{
    public static BedroomAnim Instance;
public void SwitchAnimation()
    {
        Animator animator = GetComponent<Animator>();
        Debug.Log("Before SwitchAnimation: " + animator.GetBool("IsAwake"));
        animator.enabled = true;
        
        animator.SetBool("IsAwake", true);
        Debug.Log("After SwitchAnimation: "  + animator.GetBool("IsAwake"));
    }

    public float GetAnimationLength(){
        var animator = GetComponent<Animator>();
        var time = animator.GetCurrentAnimatorStateInfo(0).length;

        return time;
    }
}
