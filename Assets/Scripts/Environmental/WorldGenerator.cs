using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private int groundSize = 100;
    [SerializeField] private float groundVerticeSeperation = 2f;
    [SerializeField] private float groundWaveHeightScale = 0.9f;

    [Header("Decoration Setup")] 
    [SerializeField] private int maxDecorPrefabs = 30;
    [SerializeField] private int minDecorPrefabs = 15;

    [Header("References")]
    [SerializeField] private GameObject ground;
    
    [SerializeField] private GameObject[] smallDecorationPrefabs;
    [SerializeField] private GameObject[] largeDecorationPrefabs;

    [SerializeField] private GameObject[] plantPrefabs;
    [SerializeField] private GameObject[] beeHivePrefabs;
    
    private void Awake()
    {
        GenerateGround();
        PlacePlants();
        PlaceDecor();
    }


    private void GenerateGround()
    {
        Mesh mesh = new Mesh();
        
        int index = 0;
        
        Vector3[] vertices = new Vector3[groundSize * groundSize];
        Vector3[] normals = new Vector3[groundSize * groundSize];

        for (int i = 0; i < groundSize; i++)
        {
            for (int j = 0; j < groundSize; j++)
            {
                float x = (float)i + 0.1f;
                float y = (float)j + 0.1f;
                
                vertices[index] = new Vector3
                (
                    groundVerticeSeperation * i - groundVerticeSeperation * groundSize / 2,
                    (Mathf.PerlinNoise((float)x, (float)y) * groundWaveHeightScale) - groundWaveHeightScale / 2,
                    groundVerticeSeperation * j - groundVerticeSeperation * groundSize/2
                );
                
                normals[index] = Vector3.up;
                
                index++;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;

        List<int> triangles = new List<int>();

        for (int i = 0; i < groundSize - 1; i++)
        {
            for (int j = 0; j < groundSize - 1; j++)
            {
                triangles.Add(i*groundSize + j);
                triangles.Add(i*groundSize + j + 1);
                triangles.Add(i*groundSize + groundSize + j);
                
                triangles.Add(i*groundSize + groundSize + j);
                triangles.Add(i*groundSize + j + 1);
                triangles.Add(i*groundSize + groundSize + j + 1);
            }
        }

        mesh.triangles = triangles.ToArray();
        
        mesh.RecalculateNormals();

        ground.GetComponent<MeshFilter>().mesh = mesh;
        ground.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void PlacePlants()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-groundSize/2 * groundVerticeSeperation, groundSize/2 * groundVerticeSeperation),
                    100, Random.Range(-groundSize/2 * groundVerticeSeperation, groundSize/2 * groundVerticeSeperation));

            //randomPos = new Vector3(Random.Range(0, 10f), 20, Random.Range(0, 10f));
            
            if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit))
            {
                Instantiate(plantPrefabs[Random.Range(0, plantPrefabs.Length - 1)], hit.point, Quaternion.identity);
            }
            else
            {
                print("No floor hit");
            }
        }
    }

    private void PlaceDecor()
    {
        int rand = Random.Range(minDecorPrefabs, maxDecorPrefabs);

        for (int i = 0; i < rand; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-groundSize/2.1f * groundVerticeSeperation, groundSize/2.1f * groundVerticeSeperation), 100, Random.Range(-groundSize/2.1f * groundVerticeSeperation, groundSize/2.1f * groundVerticeSeperation));
            
            if (i < maxDecorPrefabs * 0.7f)
            {
                if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit))
                {
                    Instantiate(smallDecorationPrefabs[Random.Range(0, smallDecorationPrefabs.Length - 1)], hit.point, Quaternion.identity);
                }
            }
            else
            {
                if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit))
                {
                    Instantiate(largeDecorationPrefabs[Random.Range(0, largeDecorationPrefabs.Length - 1)], hit.point, Quaternion.identity);
                }
            }
        }
    }
}
