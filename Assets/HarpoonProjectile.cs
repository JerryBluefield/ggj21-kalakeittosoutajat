using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonProjectile : Mirror.NetworkBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Vector3 newStartPos, Vector3 newEndPos)
    {
        startPos = newStartPos;
        endPos = newEndPos;

        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        float speed = 4;
        while(transform.position != endPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
            yield return null;
        }
        Debug.Log("Hit: " + "nothing");
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>() != null)
        {
            if (!other.GetComponent<PlayerActions>().HasHarpoon)
            {
                StopAllCoroutines();
                Debug.Log("Hit: " + other.gameObject.name);
                gameObject.SetActive(false);
            }
        }
        else
        {
            StopAllCoroutines();
            Debug.Log("Hit: " + other.gameObject.name);
            //gameObject.SetActive(false);
            GetComponent<Collider>().enabled = false;
        }

    }
}
