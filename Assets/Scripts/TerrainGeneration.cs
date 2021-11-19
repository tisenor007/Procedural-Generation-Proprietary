using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainGeneration : MonoBehaviour
{
    //VARIABLES:
    [Header("No bigger than 35 for each, higher could overload game")]
    public int mapSizeX;
    public int mapSizeZ;
    [Header("Between 0.01 and 0.1 for better results")]
    public float freq;
    [Header("Between 2 and 30, for better results")]
    public float amp;
    public int blockSpacing;
    public GameObject[] layers = new GameObject[0];
    public Text mapXTxt;
    public Text mapZTxt;
    public Text freqTxt;
    public Text ampTxt;
    public Text blockSpacingTxt;
    private List<GameObject> cubes = new List<GameObject>();
    private GameObject map;
    private float maxDepth;
    private bool maploaded;
    private float y;

    // Start is called before the first frame update
    //Generates terrain on start (self explanitory)
    void Start()
    {
        GenerateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        //keeps text updated
        mapXTxt.text = "New Map X: " + mapSizeX;
        mapZTxt.text = "New Map Z: " + mapSizeZ;
        freqTxt.text = "New Hill Variation: " + freq;
        ampTxt.text = "New Hill Heights: " + amp;
        blockSpacingTxt.text = "New Block Spacing: " + blockSpacing;
    }
   
    public void GenerateTerrain()
    {
        maploaded = false;
        //resets everything when new map is generated
        Destroy(map);
        for (int i = 0; i < cubes.Count; i++) { Destroy(cubes[i]); cubes[i] = null; }
        map = new GameObject("Map");
        for (int c = 0; c < layers.Length; c++)
        {
            GenerateBlockLayer(c);
        }
        //finds lowest point then fills map to lowest point
        maxDepth = FindLowestPoint();
        FillChunkBottom();
        foreach (GameObject cube in cubes) {
            if (cube != null) {
                //adds all blocks to a parent object for easy moving...
                cube.transform.parent = map.transform;
                for (int i = 0; i< cube.transform.childCount; i++) { cube.transform.GetChild(i).gameObject.SetActive(true); }
            } 
        }
        maploaded = true;
        //will not do backface culling until all blocks are generated
        //the bool is just an extra layer of protection
        if (maploaded == true) { FreeRendering(); }
    }

    //generates a layer of blockentered in
    public void GenerateBlockLayer(int layerNumber)
    {
        //float y;
        for (int z = 0; z < mapSizeZ; z++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                y = Mathf.PerlinNoise(x * freq, z * freq) * amp;
                cubes.Add(Instantiate(layers[layerNumber], new Vector3(x * blockSpacing, (int)y - layerNumber, z * blockSpacing), Quaternion.identity));
                foreach (GameObject cube in cubes)
                {
                    if (cube! != null)
                    {
                        if (cube.transform.position == new Vector3(x * blockSpacing, (int)y - layerNumber, z * blockSpacing))
                        {
                            cube.name = cube.name + (layerNumber + 1).ToString();
                        }
                    }
                }
            }
        }
    }

    //Fills in below of map, to the lowest point
    public void FillChunkBottom()
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i] != null)
            {
                if (cubes[i].name == layers[layers.Length-1].name + "(Clone)" + (layers.Length).ToString() && cubes[i].transform.position.y > maxDepth)
                {
                    cubes.Add(Instantiate(layers[layers.Length-1], new Vector3(cubes[i].transform.position.x, cubes[i].transform.position.y - 1, cubes[i].transform.position.z), Quaternion.identity));
                    foreach (GameObject cube in cubes)
                    {
                        if (cube != null)
                        {
                            if (cube.transform.position == new Vector3(cubes[i].transform.position.x, cubes[i].transform.position.y - 1, cubes[i].transform.position.z))
                            {
                                cube.name = cube.name + (layers.Length).ToString();
                            }
                        }
                    }
                }
            }
        }
    }

    //Finds Lowest point of Generated map
    public float FindLowestPoint()
    {
        float lowestPoint = 20;
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i] != null)
            {
                if (cubes[i].transform.position.y <= lowestPoint)
                {
                    lowestPoint = cubes[i].transform.position.y;
                }
            }
        }
        return lowestPoint;
    }

    //MAKES LOAD TIME SLOWER BUT RENDERING FASTER
    public void FreeRendering()
    {
        //manual back-face culling for performance enhancing 
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i] != null)
            {
                if (cubes[i].name != layers[0].name){if (isBlockUp(cubes[i])) { cubes[i].transform.GetChild(4).gameObject.SetActive(false); }}
                if (isBlockDown(cubes[i])) { cubes[i].transform.GetChild(5).gameObject.SetActive(false); }
                if (isBlockRight(cubes[i])) { cubes[i].transform.GetChild(3).gameObject.SetActive(false); }
                if (isBlockLeft(cubes[i])) { cubes[i].transform.GetChild(2).gameObject.SetActive(false); }
                if (isBlockFront(cubes[i])) { cubes[i].transform.GetChild(1).gameObject.SetActive(false); }
                if (isBlockBehind(cubes[i])) { cubes[i].transform.GetChild(0).gameObject.SetActive(false); }
            }
        }
    }

    //checks if a block is above said block
    public bool isBlockUp(GameObject cube)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            
            if (cubes[i] != null && cubes[i].transform.position == new Vector3(cube.transform.position.x, cube.transform.position.y + 1, cube.transform.position.z) )
            {
                return true;
            }
            
        }
        return false;
    }

    //checks if a block is below said block
    public bool isBlockDown(GameObject cube)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i] != null && cubes[i].transform.position == new Vector3(cube.transform.position.x, cube.transform.position.y - 1, cube.transform.position.z))
            {
                return true;
            }
        }
        return false;
    }

    //checks if a block is to the left of said block
    public bool isBlockLeft(GameObject cube)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i] != null && cubes[i].transform.position == new Vector3(cube.transform.position.x-1, cube.transform.position.y, cube.transform.position.z))
            {
                return true;
            }
        }
        return false;
    }

    //checks if a block is to the right of said block
    public bool isBlockRight(GameObject cube)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i] != null && cubes[i].transform.position == new Vector3(cube.transform.position.x + 1, cube.transform.position.y, cube.transform.position.z))
            {
                return true;
            }
        }
        return false;
    }

    //checks if a block is infront of said block
    public bool isBlockFront(GameObject cube)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i] != null && cubes[i].transform.position == new Vector3(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z + 1))
            {
                return true;
            }
        }
        return false;
    }

    //checks if a block behind of said block
    public bool isBlockBehind(GameObject cube)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i] != null && cubes[i].transform.position == new Vector3(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z - 1))
            {
                return true;
            }
        }
        return false;
    }


    //Button Methods
    public void AdjustMapSizeX(float value)
    {
        mapSizeX = (int)value;
    }
    public void AdjustMapSizeZ(float value)
    {
        mapSizeZ = (int)value;
    }
    public void AdjustFreq(float value)
    {
        freq = value;
    }
    public void AdjustAmp(float value)
    {
        amp = value;
    }
    public void AdjustBlockSpacing(float value)
    {
        blockSpacing = (int)value;
    }
}
