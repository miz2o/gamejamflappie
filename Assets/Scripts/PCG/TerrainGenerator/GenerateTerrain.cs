using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    [SerializeField] int width = 10;
    [SerializeField] int height = 10;

    [SerializeField] float xOffset, zOffset;
    [SerializeField] float noiseScale = 0.03f;
    [SerializeField] float heightMultiplier = 7;

    float minTerrainHeight;
    float maxTerrainHeight;

    [SerializeField] Gradient terrainGradient;
    [SerializeField] Material material;

    private Mesh mesh;
    private Texture2D gradientTexture;

    Color[] colours;
    Vector3[] vertices;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        TerrainGenerate();
        Colours();

        xOffset = Random.Range(0, 99999);
        zOffset = Random.Range(0, 99999);
    }

    void Update()
    {
        TerrainGenerate();
        Colours();

        minTerrainHeight = mesh.bounds.min.y + transform.position.y - 0.1f;
        maxTerrainHeight = mesh.bounds.max.y + transform.position.y + 0.1f;

        

        material.SetTexture("TerrainMaterial", gradientTexture);

        material.SetFloat("minTerrainHeight", minTerrainHeight);
        material.SetFloat("maxTerrainHeight", maxTerrainHeight);

        zOffset += 0.001f;
    }

    private void Colours()
    {
        gradientTexture = new Texture2D(1, 100);
        colours = new Color[vertices.Length];

        for (int i = 0, z = 0; z < vertices.Length; z++)
        {
            float colourHeight = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
            colours[i] = terrainGradient.Evaluate(colourHeight);
            i++;
        }

        gradientTexture.SetPixels(colours);
        gradientTexture.Apply();


        //colours = new Color[vertices.Length];


    }

    private void TerrainGenerate()
    {
        //VERTICES
        vertices = new Vector3[(width + 1) * (height + 1)];

        int i = 0;
        for (int z = 0; z <= height; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                float yPos = Mathf.PerlinNoise((x + xOffset) * noiseScale, (z + zOffset) * noiseScale) * heightMultiplier;
                vertices[i] = new Vector3(x, yPos, z);
                i++;
            }
        }

        //TRIANGLES
        //generating triangles
        //we are making a square which has 2 triangles and every tri has 3 points thats why where multiplying by 6
        int[] triangles = new int[width * height * 6];

        int vertex = 0;
        int triangleIndex = 0;

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                triangles[triangleIndex + 0] = vertex + 0;
                triangles[triangleIndex + 1] = vertex + width + 1;
                triangles[triangleIndex + 2] = vertex + 1;

                triangles[triangleIndex + 3] = vertex + 1;
                triangles[triangleIndex + 4] = vertex + width + 1;
                triangles[triangleIndex + 5] = vertex + width + 2;

                vertex++;
                triangleIndex += 6;
            }
            vertex++;
        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colours;
        mesh.RecalculateNormals();
    }

    /*public int width = 100, height = 100;
   // public float depth = 20;

    public float noiseScale = 0.03f;
    public float heightMultiplier = 7;
    public Mesh mesh;
    Vector3[] vertices;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        TerrainGenerator();
    }

    void Update()
    {
        TerrainGenerator();
    }

    public void TerrainGenerator()
    {
        vertices = new Vector3[(width + 1) * (height + 1)];

        int i = 0;
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                float yPos = Mathf.PerlinNoise(x * noiseScale, z * noiseScale) * heightMultiplier;

                vertices[i] = new Vector3(x, yPos, z);
                i++;
            }
        }

        //TRIANGLES
        int[] triangles = new int[width * height * 6];

        int vertex = 0;
        int triangleIndex = 0;

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                triangles[triangleIndex + 0] = vertex + 0;
                triangles[triangleIndex + 1] = vertex + width + 1;
                triangles[triangleIndex + 2] = vertex + 1;

                triangles[triangleIndex + 3] = vertex + 1;
                triangles[triangleIndex + 4] = vertex + width + 1;
                triangles[triangleIndex + 5] = vertex + width + 2;

                vertex++;
                triangleIndex += 6;
            }
            vertex++;
        }
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }*/
}
