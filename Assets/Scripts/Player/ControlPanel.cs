using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
    [Header("Setup")]
    public int groundSize; // = 100;
    public float groundVerticeSeperation; // = 2f;
    public float groundWaveHeightScale; // = 0.9f;
    
    [Header("Decoration Setup")] 
    public int smallDecor; // = 30;
    public int largeDecor; // = 15;

*/

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private Slider groundSize;
    [SerializeField] private Slider groundHeight;
    [SerializeField] private Slider smallDecor;
    [SerializeField] private Slider largeDecor;
    [SerializeField] private Slider hiveCount;
    
    public WorldGenerator WorldGenerator;
    [SerializeField] private worldGenSettings Settings = new worldGenSettings();

    public static ControlPanel instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(this);
    }

    public void SetWorldGenValues()
    {
        Settings.groundSize = Mathf.RoundToInt(groundSize.value);
        Settings.groundWaveHeightScale = groundHeight.value;

        Settings.groundVerticeSeperation = 2f;

        Settings.smallDecor = Mathf.RoundToInt(smallDecor.value);
        Settings.largeDecor = Mathf.RoundToInt(largeDecor.value);

        Settings.hiveCount = Mathf.RoundToInt(hiveCount.value);

        WorldGenerator.settings = Settings;
    }

    public void TriggerResetWorld()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TriggerWorldGenerations()
    {
        SetWorldGenValues();
        WorldGenerator.GenerateWorld();
        Camera.main.transform.position = new Vector3(0, groundSize.value, -groundSize.value * 1.5f);
        Camera.main.transform.LookAt(WorldGenerator.transform);
    }
}
