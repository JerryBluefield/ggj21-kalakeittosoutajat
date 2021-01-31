using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] bool keyDown;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    void Update()
    {
        animator.SetBool("isSelected", true);
        if (Input.GetAxis("Submit") == 1)
        {
            animator.SetBool("isPressed", true);
        }
        else if (animator.GetBool("isPressed"))
        {
            animator.SetBool("isPressed", false);
            animatorFunctions.disableOnce = true;
            select(0);
        }
    }

    public void select(int index)
    {
        Debug.Log("Game over index select: " + index);
        if (index == 0)
        {
            // exit to main menu
            SceneManager.LoadScene("StartMenu");
        }
        else
        {
            Debug.Log("invalid button index");
        }
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        select(0);
    }
}
