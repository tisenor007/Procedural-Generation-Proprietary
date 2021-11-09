using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [Header("No bigger than 35 for each, could break game")]
    public int mapSizeX;
    public int mapSizeZ;
    [Header("Between 0.01 and 0.1 for better results")]
    public float freq;
    [Header("Between 2 and 30, for better results")]
    public float amp;
    public int chunkSpacing;
    public GameObject prefab;
    private List<GameObject> cubes = new List<GameObject>();
    private GameObject map;
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
        Destroy(map);
        foreach (GameObject cube in cubes) { Destroy(cube); }
        map = new GameObject("Map");
        
        for (int z = 0; z < mapSizeZ; z++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                float y = Mathf.PerlinNoise(x * freq, z * freq) * amp;
                cubes.Add(Instantiate(prefab, new Vector3(x * chunkSpacing, y, z * chunkSpacing), Quaternion.identity));//GameObject.CreatePrimitive(PrimitiveType.Cube);
                //foreach (GameObject cube in cubes) { cube.transform.GetChild(0).gameObject.SetActive(false); }
            }
        }

        foreach (GameObject cube in cubes) { if (cube != null) { cube.transform.parent = map.transform; } }
    }
  
   
}
