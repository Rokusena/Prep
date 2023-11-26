using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Terrain))]
public class meshManipulator : MonoBehaviour
{
    private Terrain terrain;
    private TerrainData terrainData;
    private float[,] heightMap;
    private int resolution;


    private const int rocks = 0;
    private const int rocks1 = 1;
    private const int sand = 2;
    private const int grass = 3;
    private float[,,] alphaData;


    // noise settings
    public float scale = 200;
    public Vector3 size = new Vector3(100, 30, 100);
    public int octaves = 3;


    public GameObject treePrefab;


    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;
        resolution = terrainData.heightmapResolution;

        terrainData.size = size;
        GenerateHeightMap();
        AddTexture();
    }

    void GenerateHeightMap()
    {
        heightMap = new float[resolution, resolution];

        var seed = Random.Range(0, 1000);

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                for (int o = 0; o < octaves; o++)
                {
                    var px = (x + seed) / scale * math.pow(2, o);
                    var py = (y + seed) / scale * math.pow(2, o);
                    var sign = o % 2 == 0 ? 1 : -1;

                    var noiseValue = (noise.snoise(new float2(px, py)) + 1) / 2 / math.pow(2, 0);

                    heightMap[x, y] += noiseValue * sign;
                }

            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    void AddTexture()
    {

        alphaData = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                alphaData[x, y, rocks] = 0;
                alphaData[x, y, rocks1] = 0;
                alphaData[x, y, sand] = 0;
                alphaData[x, y, grass] = 0;

                if (heightMap[x, y] < 0.2f)
                {
                    alphaData[x, y, rocks] = 1;
                   
                 


                }
                else if (heightMap[x, y] < 0.4f)
                {
                    alphaData[x, y, rocks1] = 1;
                }
                else if (heightMap[x, y] < 0.7f)
                {
                    alphaData[x, y, sand] = 1;
                    float spawnChance = Random.Range(0f, 1f);
                    if (spawnChance < 0.0008f)
                    {
                       
                        Vector3 spawnPosition = new Vector3(x * size.x / terrainData.alphamapWidth, heightMap[x, y] * size.y, y * size.z / terrainData.alphamapHeight);
                        Instantiate(treePrefab, spawnPosition, Quaternion.identity);


                    }
                }
                else
                {
                    alphaData[x, y, grass] = 1;
                   
                }
            }
        }




        terrainData.SetAlphamaps(0, 0, alphaData);
    }
}