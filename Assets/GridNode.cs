using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GridNode : MonoBehaviour
{
    public float scale = 1;
    private GridObject _occupiedBy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Occupied()
    {
        return _occupiedBy != null;
    }
    
    [CanBeNull]
    public GridNode GetNodeInDirection(Vector3 direction)
    {
        var hits = Physics.RaycastAll(transform.position, direction, scale * .5f);
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("GridNode"))
                {
                    return hit.collider.GetComponent<GridNode>();
                }
            }

            return null;
        }
        else
        {
            return null;
        }
    }

    public void Occupy(GridObject gridObject)
    {
        _occupiedBy = gridObject;
    }
}
