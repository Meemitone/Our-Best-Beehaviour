using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalData : MonoBehaviour
{
    static EnvironmentalData Instance;
    [SerializeField] private Vector2 windXOffset, windZOffset;
    [SerializeField] private Vector2 windXOffsetDelta, windZOffsetDelta;
    [SerializeField] float windAreaScale;

    public static EnvironmentalData instance
    {
        get
        {
            if (Instance != null)
            {
                return Instance;
            }
            else
            {
                Debug.LogError("No Sunlight Installed, instantiating");
                Light[] lights = FindObjectsOfType<Light>();
                foreach (Light l in lights)
                {
                    if (l.type == LightType.Directional)
                    {
                        if (!l.gameObject.TryGetComponent<EnvironmentalData>(out Instance))
                        {
                            Instance = l.gameObject.AddComponent<EnvironmentalData>();
                        }
                        return Instance;
                    }
                }
                Debug.LogError("No Directional Light in Scene for Sunlight to attach to");
                GameObject gO = new GameObject();
                Light light = gO.AddComponent<Light>();
                light.type = LightType.Directional;
                Instance = gO.AddComponent<EnvironmentalData>();
                return Instance;
            }
        }
        private set
        {
            if (Instance != null)
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
        windXOffset += windXOffsetDelta * Time.deltaTime;
        windZOffset += windZOffsetDelta * Time.deltaTime;
    }

    internal Vector3 GetWind(Vector3 pos)
    {
        float x, z;
        x = Mathf.PerlinNoise((windXOffset.x + pos.x) * windAreaScale, (windXOffset.y + pos.z) * windAreaScale);
        z = Mathf.PerlinNoise((windZOffset.x + pos.x) * windAreaScale, (windZOffset.y + pos.z) * windAreaScale);

        x -= 0.5f;
        z -= 0.5f;

        x *= 2;
        z *= 2;

        x = Mathf.Clamp(x, -1, 1);
        z = Mathf.Clamp(z, -1, 1);

        return new Vector3(x, 0, z).normalized;
    }
}
