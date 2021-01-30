using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Node adjacentNode;
    private float startTime;
    private float movementLength;
    public float speed = 1.0F;
    private bool moving;
    Vector3 oldPos;
    Vector3 newPos;

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
        if (moving)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfJourney = distCovered / movementLength;
            if (transform.position != newPos)
            {
                transform.position = Vector3.Lerp(oldPos, newPos, fractionOfJourney);
            }
            else
            {
                oldPos = transform.position;
                moving = false;
            }
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
            startTime = Time.time;
            newPos = adjacentNode.transform.position;
            newPos.y = transform.position.y;
            movementLength = Vector3.Distance(transform.position, newPos);
            moving = true;
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
