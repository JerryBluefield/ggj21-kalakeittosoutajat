using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetAxis ("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if(index < maxIndex)
                    {
                        index++;
                    } else 
                    {
                        index = 0;
                    }
                } 
                else if(Input.GetAxis("Vertical") > 0)
                {
                    if(index > 0)
                    {
                        index--;
                    } 
                    else
                    {
                        index = maxIndex;
                    }
                }
                keyDown = true;
            }
        } 
        else
        {
            keyDown = false;
        }
    }

    public void select(int index)
    {
        Debug.Log("Main menu index select: " + index);
        if (index == 0)
        {
            // host game
            KalakeittoStatic.isHost = true;
            SceneManager.LoadScene("Main");
        }
        else if (index == 1)
        {
            // join game
        }
        else if (index == 2)
        {
            // exit
        }
        else
        {
            Debug.Log("invalid menu index");
        }
    }
}
