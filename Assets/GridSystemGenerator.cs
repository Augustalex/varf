using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemGenerator : MonoBehaviour
{
    public GameObject gridNodeTemplate;
    public bool hideNodes = false;
    
    public float scale = 1;
    public int xSize = 10;
    public int zSize = 10;
    
    void Start()
    {
        var startZ = transform.position.z;
        var position = transform.position;
        for (var x = 0; x < xSize; x++)
        {
            for (int y = 0; y < zSize; y++)
            {
                var node = Instantiate(gridNodeTemplate, transform);
                node.transform.position = position - Vector3.up * .5f;

                var gridNode = node.GetComponent<GridNode>();
                gridNode.scale = scale;
                gridNode.hideNode = hideNodes;
                
                position += Vector3.back;
            }
            
            position += Vector3.right;
            position = new Vector3(position.x, position.y, startZ);
        }
    }
}
