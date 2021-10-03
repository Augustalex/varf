using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class OfficePointer : MonoBehaviour
{
    public GameObject pointer;

    private Camera _camera;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (_gameManager.currentPhase != GameManager.GamePhase.Planning) return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        var isStamp = pointer && pointer.GetComponent<Stamp>() != null;

        // if (isStamp)
        // {
        //     if (pointer && !Input.GetMouseButton(0))
        //     {
        //         RaycastHit[] hits = Physics.RaycastAll(ray, 50f);
        //         if (hits.Length > 0)
        //         {
        //             foreach (var hit in hits)
        //             {
        //                 if (hit.collider.CompareTag("Desk") || hit.collider.CompareTag("TrashArea"))
        //                 {
        //                     pointer.transform.position = hit.point + Vector3.up * 2;
        //                 }
        //             }
        //         }
        //     }
        //
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         RaycastHit hit;
        //         if (Physics.Raycast(ray, out hit, 50f))
        //         {
        //             if (hit.collider.CompareTag("Desk"))
        //             {
        //                 pointer = null;
        //             }
        //         }
        //     }
        // }
        // else
        // {
            if (pointer)
            {
                RaycastHit[] hits = Physics.RaycastAll(ray, 50f);
                if (hits.Length > 0)
                {
                    foreach (var hit in hits)
                    {
                        if (hit.collider.CompareTag("Desk") || hit.collider.CompareTag("TrashArea"))
                        {
                            pointer.transform.position = hit.point + Vector3.up * 2;
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (pointer)
                {
                    pointer = null;
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50f))
                {
                    if (hit.collider.CompareTag("Item"))
                    {
                        pointer = hit.collider.gameObject;
                    }
                    else if (hit.collider.GetComponent<Clickable>())
                    {
                        hit.collider.GetComponent<Clickable>().OnClick();
                    }
                }
            }
        // }
    }

    public void ReloadCamera()
    {
        _camera = FindObjectOfType<OfficeCamera>().gameObject.GetComponent<Camera>();
    }
}