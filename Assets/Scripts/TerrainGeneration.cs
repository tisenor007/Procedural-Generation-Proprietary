using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int mapSizeX;
    public int mapSizeZ;
    private GameObject[] cubes;
    // Start is called before the first frame update
    void Start()
    {
        cubes = new GameObject[mapSizeX * mapSizeZ];
        GenerateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTerrain();
    }
    public void GenerateTerrain()
    {

        //for (int z = 0; z <= mapSizeZ; z++)
        //{
        //    for (int x = 0; x <= mapSizeX; x++)
        //    {
        //        Instantiate(cube, new Vector3(x, PerlinNoise(Time.time, 0.1f * freq) * amp, z), Quaternion.identity);
        //    }
        //}
        for (int i = 0; i <= mapSizeZ * mapSizeX - 1; i++)
        {
            for (int z = 0; z <= mapSizeZ; z++)
            {
                for (int x = 0; x <= mapSizeX; x++)
                {
                    float y = Mathf.PerlinNoise(x * .06f, z * .06f);
                    cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubes[i].transform.position = new Vector3(x, y, z);
                }
            }
        }
        Debug.Log(cubes.Length);
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
