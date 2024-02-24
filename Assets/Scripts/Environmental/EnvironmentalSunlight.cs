using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalSunlight : MonoBehaviour
{
    static EnvironmentalSunlight Instance;

    public static EnvironmentalSunlight instance
    {
        get 
        {
            if(Instance!=null)
            {
                return Instance;
            }
            else
            {
                Debug.LogError("No Sunlight Installed, instantiating");
                Light[] lights = FindObjectsOfType<Light>();
                foreach(Light l in lights)
                {
                    if(l.type == LightType.Directional)
                    {
                        if (!l.gameObject.TryGetComponent<EnvironmentalSunlight>(out Instance))
                        {
                            Instance = l.gameObject.AddComponent<EnvironmentalSunlight>();
                        }
                        return Instance;
                    }
                }
                Debug.LogError("No Directional Light in Scene for Sunlight to attach to");
                GameObject gO = new GameObject();
                Light light = gO.AddComponent<Light>();
                light.type = LightType.Directional;
                Instance = gO.AddComponent<EnvironmentalSunlight>();
                return Instance;
            }
        }
        private set
        {
            if(Instance!=null)
            {
                Instance = value;
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
