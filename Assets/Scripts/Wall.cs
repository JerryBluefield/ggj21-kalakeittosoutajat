using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Collider col;
    private Coroutine moveCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            Up();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            Down();
        }
    }

    private void Up()
    {
        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        moveCoroutine = StartCoroutine(UpCoroutine());
    }

    private void Down()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        moveCoroutine = StartCoroutine(DownCoroutine());
    }

    private IEnumerator UpCoroutine()
    {
        float targetY = 0;
        while (transform.position.y != targetY)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), Time.deltaTime * 2);
            yield return null;
        }
        col.enabled = true;
    }

    private IEnumerator DownCoroutine()
    {
        float targetY = -1.93f;
        while(transform.position.y != targetY)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z),Time.deltaTime*2);
            yield return null;
        }
        col.enabled = false;
    }
}
