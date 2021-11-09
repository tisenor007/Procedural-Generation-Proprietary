using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int mapSizeX;
    public int mapSizeZ;
    public GameObject prefab;
    private List<GameObject> cubes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateTerrain();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateTerrain();
        }
    }
   
    public void GenerateTerrain()
    {
        foreach (GameObject cube in cubes) { Destroy(cube); }
        for (int z = 0; z < mapSizeZ; z++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                float y = Mathf.PerlinNoise(x * (UnityEngine.Random.Range(0.06f, 0.5f)), z * (UnityEngine.Random.Range(0.06f, 0.5f)));
                cubes.Add(Instantiate(prefab, new Vector3(x * 2, y, z * 2), Quaternion.identity));//GameObject.CreatePrimitive(PrimitiveType.Cube);
                //foreach (GameObject cube in cubes) { cube.transform.GetChild(0).gameObject.SetActive(false); }
            }
        }
        

        Debug.Log(mapSizeZ * mapSizeX);
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
