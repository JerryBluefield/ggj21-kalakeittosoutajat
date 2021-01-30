using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Mesh> wallModels;
    // Start is called before the first frame update
    void Start()
    {
        RandomizeWallModels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomizeWallModels()
    {
        var walls = GetComponentsInChildren<Wall>();

        for (int i = 0; i < walls.Length; i++)
        {
            if (!walls[i].OuterWall)
            {
                int randomIndex = Random.Range(0, wallModels.Count);
                walls[i].GetComponent<MeshFilter>().mesh = wallModels[randomIndex];
            }
        }

    }
}
