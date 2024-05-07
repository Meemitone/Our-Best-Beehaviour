using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public struct worldGenSettings
{
    [Header("Setup")]
    public int groundSize; // = 100;
    public float groundVerticeSeperation; // = 2f;
    public float groundWaveHeightScale; // = 0.9f;
    public int hiveCount; // = 3;
    
    [Header("Decoration Setup")] 
    public int smallDecor; // = 30;
    public int largeDecor; // = 15;
}

public class WorldGenerator : MonoBehaviour
{
    public worldGenSettings settings;

    [Header("References")]
    [SerializeField] private GameObject ground;
    
    [SerializeField] private GameObject[] smallDecorationPrefabs;
    [SerializeField] private GameObject[] largeDecorationPrefabs;

    [SerializeField] private GameObject[] plantPrefabs;
    [SerializeField] private GameObject beeHivePrefab;

    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        FindObjectOfType<ControlPanel>().WorldGenerator = this;
        FindObjectOfType<ControlPanel>().SetWorldGenValues();
    }

    public void GenerateWorld()
    {
        GenerateGround();
     
        PlacePlants();
        PlaceDecor();
        SetUpBoxes();
        PlaceHives();

    }

    private void GenerateGround()
    {
        Mesh mesh = new Mesh();
        
        int index = 0;
        
        Vector3[] vertices = new Vector3[settings.groundSize * settings.groundSize];
        Vector3[] normals = new Vector3[settings.groundSize * settings.groundSize];

        for (int i = 0; i < settings.groundSize; i++)
        {
            for (int j = 0; j < settings.groundSize; j++)
            {
                float x = (float)i + 0.1f;
                float y = (float)j + 0.1f;
                
                vertices[index] = new Vector3
                (
                    settings.groundVerticeSeperation * i - settings.groundVerticeSeperation * settings.groundSize / 2,
                    (Mathf.PerlinNoise((float)x*0.04f, (float)y*0.04f) * settings.groundWaveHeightScale * 2) - settings.groundWaveHeightScale / 2,
                    settings.groundVerticeSeperation * j - settings.groundVerticeSeperation * settings.groundSize/2
                );
                
                normals[index] = Vector3.up;
                
                index++;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;

        List<int> triangles = new List<int>();

        for (int i = 0; i < settings.groundSize - 1; i++)
        {
            for (int j = 0; j < settings.groundSize - 1; j++)
            {
                triangles.Add(i*settings.groundSize + j);
                triangles.Add(i*settings.groundSize + j + 1);
                triangles.Add(i*settings.groundSize + settings.groundSize + j);
                
                triangles.Add(i*settings.groundSize + settings.groundSize + j);
                triangles.Add(i*settings.groundSize + j + 1);
                triangles.Add(i*settings.groundSize + settings.groundSize + j + 1);
            }
        }

        mesh.triangles = triangles.ToArray();
        
        mesh.RecalculateNormals();

        if (ground == null)
        {
            Debug.LogError("Ground not assigned");
            return;
        }
        
        ground.GetComponent<MeshFilter>().mesh = mesh;
        ground.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void PlacePlants()
    {
        GameObject newParent = new GameObject();
        newParent.name = "Plants";
        
        for (int i = 0; i < 20; i++)
        {
            if (Physics.Raycast(randomPos(), Vector3.down, out RaycastHit hit, groundLayer))
            {
                Instantiate(plantPrefabs[Random.Range(0, plantPrefabs.Length - 1)], hit.point, Quaternion.identity, newParent.transform);
            }
        }
    }

    private void PlaceDecor()
    {
        GameObject newParent = new GameObject();
        newParent.name = "Small Decor";
        
        for (int i = 0; i < settings.smallDecor; i++)
        {
            if (Physics.Raycast(randomPos(), Vector3.down, out RaycastHit hit, groundLayer))
            {
                Instantiate(smallDecorationPrefabs[Random.Range(0, smallDecorationPrefabs.Length - 1)], hit.point, Quaternion.identity ,newParent.transform);
            }
        }
        
        newParent = new GameObject();
        newParent.name = "Large Decor";

        for (int i = 0; i < settings.largeDecor; i++)
        {
           
            
            if (Physics.Raycast(randomPos(), Vector3.down, out RaycastHit hit , groundLayer))
            {
                Instantiate(largeDecorationPrefabs[Random.Range(0, largeDecorationPrefabs.Length - 1)], hit.point, Quaternion.identity ,newParent.transform);
            }
        }
    }

    private void PlaceHives()
    {
        GameObject newParent = new GameObject();
        newParent.name = "Hives";
        
        for (int i = 0; i < settings.hiveCount; i++)
        {
            if (Physics.Raycast(randomPos(), Vector3.down, out RaycastHit hit, groundLayer))
            {
                Instantiate(beeHivePrefab, hit.point, quaternion.identity, newParent.transform);
            }
        }
    }

    private void SetUpBoxes()
    {
        int scale = Mathf.RoundToInt(settings.groundVerticeSeperation * 5);
        
        BoxGenerator boxGenerator = FindObjectOfType<BoxGenerator>();

        boxGenerator.numberOnAxis = new Vector2(settings.groundSize / (scale/2), settings.groundSize / (scale/2));
        
        boxGenerator.transform.position += Vector3.up *settings.groundSize / 2;

        boxGenerator.scale = scale;
        
        boxGenerator.GenerateBoxes();
    }

    private Vector3 randomPos()
    {
        return new Vector3
        (
            Random.Range(-settings.groundSize/2.1f * settings.groundVerticeSeperation, settings.groundSize/2.1f * settings.groundVerticeSeperation), 
            100, 
            Random.Range(-settings.groundSize/2.1f * settings.groundVerticeSeperation, settings.groundSize/2.1f * settings.groundVerticeSeperation)
        );
    }
}
