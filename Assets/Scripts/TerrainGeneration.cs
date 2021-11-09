using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainGeneration : MonoBehaviour
{
    [Header("No bigger than 35 for each, could break game")]
    public int mapSizeX;
    public int mapSizeZ;
    [Header("Between 0.01 and 0.1 for better results")]
    public float freq;
    [Header("Between 2 and 30, for better results")]
    public float amp;
    public int blockSpacing;
    public GameObject prefab;
    public Text mapXTxt;
    public Text mapZTxt;
    public Text freqTxt;
    public Text ampTxt;
    public Text blockSpacingTxt;
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
        mapXTxt.text = "New Map X: " + mapSizeX;
        mapZTxt.text = "New Map Z: " + mapSizeZ;
        freqTxt.text = "New Frequency: " + freq;
        ampTxt.text = "New Amp: " + amp;
        blockSpacingTxt.text = "New Block Spacing: " + blockSpacing;
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
                cubes.Add(Instantiate(prefab, new Vector3(x * blockSpacing, y, z * blockSpacing), Quaternion.identity));//GameObject.CreatePrimitive(PrimitiveType.Cube);
                //foreach (GameObject cube in cubes) { cube.transform.GetChild(0).gameObject.SetActive(false); }
            }
        }

        foreach (GameObject cube in cubes) { if (cube != null) { cube.transform.parent = map.transform; } }
    }

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
