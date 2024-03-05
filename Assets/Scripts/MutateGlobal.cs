using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutateGlobal : MonoBehaviour
{
    static MutateGlobal Instance;

    [SerializeField] internal float plantMutateChance, 
        //these three are for specifically changing the growth structure
        plantAddChance, plantDeleteChance, plantReplaceChance;
    [SerializeField] internal float plantCharLChance, plantCharFChance, plantCharSChance;
    public static MutateGlobal instance
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
                        if (!l.gameObject.TryGetComponent<MutateGlobal>(out Instance))
                        {
                            Instance = l.gameObject.AddComponent<MutateGlobal>();
                        }
                        return Instance;
                    }
                }
                Debug.LogError("No Directional Light in Scene for Sunlight to attach to");
                GameObject gO = new GameObject();
                Light light = gO.AddComponent<Light>();
                light.type = LightType.Directional;
                Instance = gO.AddComponent<MutateGlobal>();
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
