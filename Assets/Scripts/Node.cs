using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private List<Node> adjacentNodes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GetAdjacentNodes();
        }
    }

    private void GetAdjacentNodes()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2))
        {
            if (hit.transform.CompareTag("Node"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
            else if (hit.transform.CompareTag("Wall"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 2))
        {
            if (hit.transform.CompareTag("Node"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
            }
            else if (hit.transform.CompareTag("Wall"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.red);
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 2))
        {
            if (hit.transform.CompareTag("Node"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
            }
            else if (hit.transform.CompareTag("Wall"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.red);
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 2))
        {
            if (hit.transform.CompareTag("Node"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
            }
            else if (hit.transform.CompareTag("Wall"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.red);
            }
        }
    }
}
