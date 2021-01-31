using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Mirror.NetworkBehaviour
{
    public PlayerMovement CurrentPlayer => currentPlayer;

    public static PlayerController Instance;


    [SerializeField] private PlayerMovement currentPlayer;
    [Header("Component Reference")]
    [SerializeField] private PlayerMovement child;
    [SerializeField] private PlayerMovement fisher;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vcam;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = child;
    }

    public void ChangeCurrentPlayer()
    {
        if(currentPlayer == child)
        {
            currentPlayer = fisher;

            vcam.Follow = currentPlayer.transform;

            Debug.Log("Current player: Fisher");
        }
        else
        {
            currentPlayer = child;

            vcam.Follow = currentPlayer.transform;

            Debug.Log("Current player: Child");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
