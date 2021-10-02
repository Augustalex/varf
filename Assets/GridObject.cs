using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Vector3 dimensions;

    public GameObject buildableGhost;
    public GameObject unbuildableGhost;

    public event Action OnBuild;
    
    private void Awake()
    {
        buildableGhost.SetActive(false);
        unbuildableGhost.SetActive(false);
    }

    void Start()
    {
            
    }

    void Update()
    {
        
    }

    public void OccupyNodes(GridNode node)
    {
        foreach (var gridNode in GetOccupiedGridNodes(node))
        {
            gridNode.Occupy(this);
        }
    }

    private List<GridNode> GetOccupiedGridNodes(GridNode node)
    {
        var nodes = new List<GridNode>();

        var rowNode = node;
        for (int x = 0; x < dimensions.x; x++)
        {
            if (!rowNode || rowNode.Occupied())
            {
                Debug.LogError("Neighbouring node was null or occupied in GetNodesInDirection. The nodes should already have been verified to exist before this step!");
            }
            nodes.Add(rowNode);
            
            var currentNode = rowNode;
            
            for (int z = 0; z < dimensions.z - 1; z++)
            {
                currentNode = currentNode.GetNodeInDirection(-node.transform.forward);
                if (!currentNode || currentNode.Occupied())
                {
                    Debug.LogError("Neighbouring node was null or occupied in GetNodesInDirection. The nodes should already have been verified to exist before this step!");
                }
                nodes.Add(currentNode);
            }
            
            rowNode = rowNode.GetNodeInDirection(-node.transform.right);
        }

        return nodes;
    }

    public bool Fits(GridNode node)
    {
        var rowNode = node;
        for (int x = 0; x < dimensions.x; x++)
        {
            if (!rowNode || rowNode.Occupied())
            {
                return false;
            }

            var currentNode = rowNode;
            
            for (int z = 0; z < dimensions.z - 1; z++)
            {
                currentNode = currentNode.GetNodeInDirection(-node.transform.forward);
                if (!currentNode || currentNode.Occupied())
                {
                    return false;
                }
            }
            
            rowNode = rowNode.GetNodeInDirection(-node.transform.right);
        }

        return true;
    }

    public void SetAbleToBuild()
    {
        buildableGhost.SetActive(true);
        unbuildableGhost.SetActive(false);
    }

    public void SetUnableToBuild()
    {
        buildableGhost.SetActive(false);
        unbuildableGhost.SetActive(true);
    }

    public void Build(GridNode node)
    {
        OccupyNodes(node);
        
        Destroy(buildableGhost);
        Destroy(unbuildableGhost);
        
        OnBuild?.Invoke();
    }
}
