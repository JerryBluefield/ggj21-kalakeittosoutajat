using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.U))
        {
            Up();
        }
        if(Input.GetKey(KeyCode.D))
        {
            Down();
        }
    }

    private void Up()
    {
        StartCoroutine(UpCoroutine());
    }

    private void Down()
    {
        StartCoroutine(DownCoroutine());
    }

    private IEnumerator UpCoroutine()
    {
        while (transform.position.y != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, transform.position.z), Time.deltaTime * 2);
            yield return null;
        }
    }

    private IEnumerator DownCoroutine()
    {
        while(transform.position.y != -2)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -2, transform.position.z),Time.deltaTime*2);
            yield return null;
        }
    }
}
