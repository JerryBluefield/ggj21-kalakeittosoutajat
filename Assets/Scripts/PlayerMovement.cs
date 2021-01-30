using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Node adjacentNode;

    [SerializeField] private float rayDistance = 4f;
    // Start is called before the first frame update
    void Start()
    {
        UpdateVision();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            UpdateVision();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveForward();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Turn(1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Turn(-1);
        }
    }

    private void Turn(int direction)
    {
        transform.eulerAngles += new Vector3(0, direction*90, 0);
        UpdateVision();
    }

    private void MoveForward()
    {
        if(adjacentNode != null)
        {
            Vector3 newPos = adjacentNode.transform.position;
            newPos.y = transform.position.y;
            transform.position = newPos;
            adjacentNode = null;
            UpdateVision();
        }
        else
        {
            Debug.Log("Can't move!");
        }

    }

    private void UpdateVision()
    {
        Vector3 rayCastStart = transform.position; 
        rayCastStart.y = 0;

        int blockLength = 2;

        int rayCount = (int) (rayDistance / 2f);
        int nodeCount = 0;

        LayerMask wallLayer = LayerMask.GetMask("Wall");

        RaycastHit hit;
        for (int i = 0; i < rayCount; i++)
        {
            float rayLength = blockLength * (1 + rayCount);
            if (Physics.Raycast(rayCastStart, transform.TransformDirection(Vector3.forward), out hit, rayLength ,wallLayer))
            {
                break;
            }
            nodeCount++;
        }

        if(nodeCount > 0)
        {
            RaycastHit[] hits;
            LayerMask floorLayer = LayerMask.GetMask("Floor");
            hits = Physics.RaycastAll(rayCastStart, transform.forward, rayDistance, floorLayer);
            //List<RaycastHit> hitsList = hits.ToList();

            RaycastHit adjacentHit;
            Physics.Raycast(rayCastStart, transform.forward, out adjacentHit, rayDistance, floorLayer);
            if(adjacentHit.transform != null)
            {
                Debug.Log("AdjacentHit: " + adjacentHit.transform.name);

                adjacentNode = adjacentHit.transform.GetComponent<Node>();
            }
            else
            {
                Debug.Log("Nothing in front!");
            }

        }
        else
        {
            Debug.Log("Wall is in front of you");
        }

        //for (int i = 0; i < nodeCount; i++)
        //{
        //    float rayLength = blockLength * (1 + rayCount);
        //    if (Physics.Raycast(rayCastStart, transform.TransformDirection(Vector3.forward), out hit, rayLength, 9))
        //    {
        //        Debug.Log("Hit: " + hit.transform.name);
        //    }
        //}

        //RaycastHit[] hits;
        //hits = Physics.RaycastAll(rayCastStart, transform.forward, rayDistance);

        //for (int i = 0; i < hits.Length; i++)
        //{
        //    // Debug.Log("Hit Name: " + hits[i].transform.name);

        //    if (hits[i].transform.CompareTag("Wall"))
        //    {
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hits[i].distance, Color.red);
        //        Debug.Log("Wall: " + hits[i].transform.name);
        //        break;
        //    }
        //    if (hits[i].transform.CompareTag("Node"))
        //    {
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hits[i].distance, Color.yellow);
        //        Debug.Log("Node: " + hits[i].transform.name);
        //    }
        //}

        //if (Physics.Raycast(rayCastStart, transform.TransformDirection(Vector3.forward), out hit, 4))
        //{
        //    if (hit.transform.CompareTag("Node"))
        //    {
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //        Debug.Log("Node: " + hit.transform.name);
        //    }
        //    else if (hit.transform.CompareTag("Wall"))
        //    {
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        //        Debug.Log("Wall: " + hit.transform.name);
        //    }
        //}
    }
}
