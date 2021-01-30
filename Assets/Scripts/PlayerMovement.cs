using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : Mirror.NetworkBehaviour
{
    private Node adjacentNode;

    [SerializeField] private float rayDistance = 4f;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float animatorStartSpeed = 0.5f;
    [SerializeField] private float animatorEndSpeed = 0.25f;
    [SerializeField] private PlayerActions actions;

    private Coroutine moveCoroutine;
    private Coroutine turnCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            GameObject cameraObj = GameObject.Find("ThirdPersonVirtualCamera");
            if (cameraObj != null)
            {
                CinemachineVirtualCamera camera = cameraObj.GetComponent<CinemachineVirtualCamera>();
                if (camera != null)
                {
                    camera.Follow = transform;
                }
                else
                {
                    Debug.Log("Failed to find component CinemachineVirtualCamera");
                }
            }
            else
            {
                Debug.Log("Failed to find obj ThirdPersonVirtualCamera");
            }
            actions = GetComponent<PlayerActions>();
            UpdateVision();
            animator.SetFloat("Moving", 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCoroutine == null)
        {
            float animationValue = Mathf.MoveTowards(animator.GetFloat("Moving"), 0f, Time.deltaTime * animatorEndSpeed);
            animator.SetFloat("Moving", animationValue);
        }

        if (!isLocalPlayer)
        {
            return;
        }

        // Moving parameter: 0 is idle / 1 is moving.
        //animator.SetFloat("Moving", Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time)));

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
        Vector3 targetEuler = transform.eulerAngles + (direction * new Vector3(0, 90, 0));
        //transform.eulerAngles += new Vector3(0, direction*90, 0);
        if(turnCoroutine == null && moveCoroutine == null)
        {
            if (actions.TurnAction())
            {
                turnCoroutine = StartCoroutine(TurnCoroutine(targetEuler));
            }
            else
            {
                Debug.Log("Not enough action points for turning!");
            }

        }
    }

    private IEnumerator TurnCoroutine(Vector3 targetEuler)
    {

        float transition = 0f;
        float speed = 1.2f;

        var startRot = transform.rotation;

        while (transition < 1f)
        {
            transition += Time.deltaTime * speed;
            transform.rotation = Quaternion.Lerp(startRot, Quaternion.Euler(targetEuler), Mathf.SmoothStep(0.0f, 1.0f, transition));

            yield return null;
        }
        adjacentNode = null;
        UpdateVision();
        turnCoroutine = null;
    }

    private void MoveForward()
    {
        if(adjacentNode != null)
        {
            if (actions.MoveForwardAction())
            {
                Vector3 newPos = adjacentNode.transform.position;
                newPos.y = transform.position.y;
                if (turnCoroutine == null && moveCoroutine == null)
                {
                    moveCoroutine = StartCoroutine(MoveCoroutine(newPos));
                }
            }
            else
            {
                Debug.Log("Not enough action points for moving!");
            }
        }
        else
        {
            Debug.Log("Can't move! Something blocks the way I guess.");
        }

    }
    IEnumerator MoveCoroutine(Vector3 newPos)
    {
        float transition = 0f;

        float movementLength = Vector3.Distance(transform.position, newPos);


        var startPos = transform.position;
        while (transition < 1f)
        {
            float animationValue = Mathf.MoveTowards(animator.GetFloat("Moving"), 1f, Time.deltaTime * animatorStartSpeed);
            animator.SetFloat("Moving", animationValue);

            transition += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, newPos, Mathf.SmoothStep(0.0f, 1.0f, transition));

            yield return null;
        }
        adjacentNode = null;
        UpdateVision();
        moveCoroutine = null;
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
            //float rayLength = blockLength * (1 + rayCount);
            float rayLength = 2;

            if (Physics.Raycast(rayCastStart, transform.TransformDirection(Vector3.forward), out hit, rayLength ,wallLayer))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
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
