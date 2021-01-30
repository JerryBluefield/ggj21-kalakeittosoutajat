using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;

    void Update()
    {
        if(menuButtonController.index == thisIndex)
        {
            animator.SetBool("isSelected", true);
            if(Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("isPressed", true);
            }
            else if (animator.GetBool("isPressed"))
            {
                animator.SetBool("isPressed", false);
                animatorFunctions.disableOnce = true;
            }
        }
        else
        {
            animator.SetBool("isSelected", false);
        }
    }
}
