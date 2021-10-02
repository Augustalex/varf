using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemGenerator : MonoBehaviour
{
    public GameObject gridNodeTemplate;

    private const int Size = 10;
    
    void Start()
    {
        var startZ = transform.position.z;
        var position = transform.position;
        for (var x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                var node = Instantiate(gridNodeTemplate);
                node.transform.position = position;
                
                position += Vector3.back;
            }
            
            position += Vector3.right;
            position = new Vector3(position.x, position.y, startZ);
        }
    }
}
