using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject pointer;
    
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("GridNode"))
                {
                    pointer.SetActive(true);
                    
                    pointer.transform.position = hit.transform.position + Vector3.up;
                    return;
                }
            }
        }
        else
        {
            pointer.SetActive(false);
        }
    }
}
