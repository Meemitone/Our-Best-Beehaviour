using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour
{
    public Vector2 numberOnAxis = new Vector2(10, 10);

    public int scale = 5;

    [SerializeField] GameObject boxPrefab;

    [SerializeField] List<Box> boxes = new List<Box>();

    public void GenerateBoxes()
    {
        Vector3 positionMath = transform.position + new Vector3(-(numberOnAxis.x/2) * scale, 0, -(numberOnAxis.y / 2) * scale);

        for (int x = 0; x < numberOnAxis.x; x++)
        {
            positionMath.x += scale;
            for(int z = 0; z < numberOnAxis.y; z++)
            {
                positionMath.z += scale;
                GameObject newBox = GameObject.Instantiate(boxPrefab, positionMath, transform.rotation, transform);

                newBox.transform.localScale = new Vector3(scale, transform.position.y * 2, scale);

                newBox.name = "Cube(" + x + ", " + z + ")";
                Box curBox = newBox.GetComponent<Box>();

                boxes.Add(curBox);

                if(x != 0)
                {
                    int index = (int)((x - 1) * numberOnAxis.y + z);
                    curBox.neighbours.Add(boxes[index]);

                    boxes[index].neighbours.Add(curBox);

                }
                if(z != 0)
                {
                    
                    curBox.neighbours.Add(boxes[boxes.Count -2]);

                    boxes[boxes.Count -2].neighbours.Add(curBox);
                }
            }
            positionMath.z -= scale * numberOnAxis.y;
        }

    }
}
