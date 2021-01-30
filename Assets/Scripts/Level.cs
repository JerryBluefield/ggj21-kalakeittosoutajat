using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Mesh> wallModels;
    [SerializeField] private List<Mesh> floorModels;
    [SerializeField] private List<Mesh> columnModels;
    // Start is called before the first frame update
    void Start()
    {
        RandomizeWallModels();
        RandomizeFloorModels();
        RandomizeColumnModels();
    }

    private void RandomizeFloorModels()
    {
        var floors = GetComponentsInChildren<Node>();

        for (int i = 0; i < floors.Length; i++)
        {
            int randomIndex = Random.Range(0, floorModels.Count);
            floors[i].GetComponent<MeshFilter>().mesh = floorModels[randomIndex];
        }
    }

    private void RandomizeColumnModels()
    {
        var columns = GetComponentsInChildren<Transform>();

        for (int i = 0; i < columns.Length; i++)
        {
            if (columns[i].CompareTag("Column"))
            {
                int randomIndex = Random.Range(0, columnModels.Count);
                columns[i].GetComponent<MeshFilter>().mesh = columnModels[randomIndex];
            }
        }
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
