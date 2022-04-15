using System.Linq;
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
                        pointer.transform.position = hit.point + Vector3.up * 2.8f;
                    }
                }
            }

            var rotateAmount = Input.mouseScrollDelta.y * 10f;
            pointer.transform.RotateAround(pointer.transform.position, Vector3.up, rotateAmount);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (pointer)
            {
                pointer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
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
                    hit.rigidbody.constraints = RigidbodyConstraints.FreezeRotation |
                                                RigidbodyConstraints.FreezePositionY;

                    var rotation = pointer.transform.rotation.eulerAngles;
                    pointer.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
                }
                else if (hit.collider.GetComponent<Clickable>())
                {
                    hit.collider.GetComponent<Clickable>().OnClick();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    if (hit.collider.GetComponentInChildren<Paper>())
                    {
                        var hits = Physics.RaycastAll(ray, 50f);
                        ShiftPile(GetPapersInOrderFromTopToBottom(hits));
                    }
                }
            }
        }

        // }
    }

    private Paper[] GetPapersInOrderFromTopToBottom(RaycastHit[] hits)
    {
        return hits.Where(hit => hit.collider.GetComponentInChildren<Paper>())
            .Select(hit => hit.collider.GetComponentInChildren<Paper>())
            .OrderBy(paper => paper.transform.position.y)
            .ToArray();
    }

    private void ShiftPile(Paper[] hits)
    {
        Debug.Log("hits: " + hits.Length);
        for (var i = 0; i < hits.Length; i++)
        {
            Debug.Log($"[{i}]: {hits[i].transform.position}");
        }

        var firstPosition = hits[0].transform.position;
        var firstRotation = hits[0].transform.rotation;
        for (var i = 0; i < hits.Length - 1; i++)
        {
            var newPosition = hits[i + 1].transform.position;
            var currentPosition = hits[i].transform.position;
            hits[i].transform.position = new Vector3(
                currentPosition.x,
                newPosition.y,
                currentPosition.z
            );
            // hits[i].transform.rotation = hits[i + 1].transform.rotation;
        }

        hits[hits.Length - 1].transform.position = firstPosition;
        // hits[hits.Length - 1].transform.rotation = firstRotation;
    }

    public void ReloadCamera()
    {
        _camera = FindObjectOfType<OfficeCamera>().gameObject.GetComponent<Camera>();
    }
}