using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtonController : MonoBehaviour
{
    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    public AudioSource audioSource;

    private Transform connect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        connect = transform.Find("Connect");
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
            connect.gameObject.SetActive(true);
        }
        else if (index == 2)
        {
            GetComponent<EnvironmentVolumeEnabler>().EnableEnvironmentVolume();
        }
        else if (index == 3)
        {
            // exit
        }
        else
        {
            Debug.Log("invalid menu index");
        }
    }

    public void joinGame()
    {
        KalakeittoStatic.joinIp = connect.GetComponentInChildren<TMP_InputField>().text;
        KalakeittoStatic.isHost = false;
        Debug.Log("Joining game: " + KalakeittoStatic.joinIp);
        SceneManager.LoadScene("Main");
    }
}
