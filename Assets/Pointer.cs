using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
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
        if (_gameManager.currentPhase != GameManager.GamePhase.Construction) return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (pointer)
        {
            var gridObject = pointer.GetComponent<GridObject>();
            if (!gridObject) return;

            RaycastHit[] hits = Physics.RaycastAll(ray, 200f);
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.collider.CompareTag("GridNode"))
                    {
                        pointer.SetActive(true);

                        var gridNode = hit.collider.GetComponent<GridNode>();
                        if (gridObject.Fits(gridNode))
                        {
                            gridObject.SetAbleToBuild();

                            pointer.transform.position = hit.transform.position + Vector3.up;

                            if (Input.GetMouseButtonDown(0))
                            {
                                pointer =
                                    null; // Note: Pointer is reassigned after a grid object is built by an event listener, so be careful not to do this after Build
                                gridObject.Build(gridNode);
                            }
                        }
                        else
                        {
                            gridObject.SetUnableToBuild();

                            pointer.transform.position = new Vector3(
                                hit.point.x,
                                hit.transform.position.y,
                                hit.point.z
                            ) + Vector3.up;
                        }

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

    public void ReloadCamera()
    {
        _camera = FindObjectOfType<WorldCamera>().gameObject.GetComponent<Camera>();
    }
}