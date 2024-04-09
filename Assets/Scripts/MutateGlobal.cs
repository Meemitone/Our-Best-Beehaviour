using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutateGlobal : MonoBehaviour
{
    static MutateGlobal instance;

    [SerializeField] internal float plantMutateChance, 
        //these three are for specifically changing the growth structure
        plantAddChance, plantDeleteChance, plantReplaceChance;
    [SerializeField] internal float plantCharLChance, plantCharFChance, plantCharSChance;
    public static MutateGlobal Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                Debug.LogError("No Sunlight Installed, instantiating");
                Light[] lights = FindObjectsOfType<Light>();
                foreach (Light l in lights)
                {
                    if (l.type == LightType.Directional)
                    {
                        if (!l.gameObject.TryGetComponent<MutateGlobal>(out instance))
                        {
                            instance = l.gameObject.AddComponent<MutateGlobal>();
                        }
                        return instance;
                    }
                }
                Debug.LogError("No Directional Light in Scene for Sunlight to attach to");
                GameObject gO = new();
                Light light = gO.AddComponent<Light>();
                light.type = LightType.Directional;
                instance = gO.AddComponent<MutateGlobal>();
                return instance;
            }
        }
        private set
        {
            if (instance != null)
            {
                instance = value;
            }
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public char GetRandomGeneCodeLetter()
    {
        float rand = Random.value;
        if (rand <= plantCharFChance)
            return 'F';
        else if (rand <= plantCharFChance + plantCharLChance)
            return 'L';
        else if (rand <= plantCharFChance + plantCharLChance + plantCharSChance)
            return 'S';
        else
            return 'N';
    }
}
