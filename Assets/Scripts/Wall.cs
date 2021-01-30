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
        while (transform.position.y != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, transform.position.z), Time.deltaTime * 2);
            yield return null;
        }
        col.enabled = true;
    }

    private IEnumerator DownCoroutine()
    {
        while(transform.position.y != -2)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -2, transform.position.z),Time.deltaTime*2);
            yield return null;
        }
        col.enabled = false;
    }
}
