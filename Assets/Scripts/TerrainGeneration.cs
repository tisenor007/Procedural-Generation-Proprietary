using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int mapSizeX;
    public int mapSizeZ;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        GenerateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTerrain();
    }
    public void GenerateTerrain()
    {
        for (int z = 0; z <= mapSizeZ; z++)
        {
            for (int x = 0; x <= mapSizeX; x++)
            {
                Instantiate(cube, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }
    public void UpdateTerrain()
    {
        //for (int z = 0; z <= mapSizeZ; z++)
        //{
        //    for (int x = 0; x <= mapSizeX; x++)
        //    {
        //        squares[x, z].transform.position = new Vector3(x, 0, z);
        //    }
        //}
    }
}
