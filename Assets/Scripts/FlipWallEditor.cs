using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlipWallEditor : MonoBehaviour
{
    [SerializeField] private bool flipped = false;
    private void Awake()
    {
        Debug.Log("Wat");
        Flip();
    }

    private void Flip()
    {
        var walls = GetComponentsInChildren<Wall>();
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i].transform.position.y == 0)
            {
                Vector3 newPos = walls[i].transform.position;
                newPos.y = -1.93f;
                walls[i].transform.position = newPos;
            }
            else if (walls[i].transform.position.y == -1.93f)
            {
                Vector3 newPos = walls[i].transform.position;
                newPos.y = 0;
                walls[i].transform.position = newPos;
            }
        }
        //if(transform.position.y == 0)
        //{
        //    Vector3 newPos = transform.position;
        //    newPos.y = -1.93f;
        //    transform.position = newPos;
        //}
        //else if(transform.position.y == -1.93f)
        //{
        //    Vector3 newPos = transform.position;
        //    newPos.y = 0;
        //    transform.position = newPos;
        //}
    }

    void Update()
    {
        if (flipped)
        {
            Flip();
            // Debug.Log("Wat");
            if (Input.GetKeyDown(KeyCode.P))
            {
                Flip();
                Debug.Log("Wat");
            }
            flipped = false;
        }

    }
}