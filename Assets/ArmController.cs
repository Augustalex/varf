using System;
using System.Linq;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    private Vector3 _previousPosition;
    private Vector3 _startPosition;

    public GameObject hand;
    public GameObject pointer;
    private bool _setted;
    public GameObject possiblePointer;
    private bool _started;
    private Vector3 _startingPosition;
    private bool _doneCorrecting;

    public event Action Grabbed;
    public event Action Hovering;
    public event Action Dropped;

    void Start()
    {
        _startingPosition = transform.position;
        // SetCursorPos(Screen.width / 2 + 500, Screen.height / 2);
        _previousPosition = Input.mousePosition;
        // _startPosition = Input.mousePosition;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!_started)
        {
            transform.position = _startingPosition;
            _previousPosition = _startingPosition;
            _started = true;
            return;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _doneCorrecting = true;
            Destroy(FindObjectOfType<StartInstructionsScreen>().gameObject);
            FindObjectOfType<thankyou>().GetComponent<AudioSource>().Play();
            Cursor.visible = false;
        }
        else if (!_doneCorrecting || Input.GetKey(KeyCode.Space))
        {
            Cursor.visible = true;
            transform.position = _startingPosition;
        }

        // if (!_setted && (Input.mousePosition - _startPosition).magnitude > 400)
        // {
        //     // Debug.Log("NOW: " + Input.mousePosition + " - FROM: " + _startPosition + " - MAG: " +
        //     //           (Input.mousePosition - _startPosition).magnitude);
        //     SetCursorPos(Screen.width / 2 + 500, Screen.height / 2);
        //     _setted = true;
        //     return;
        // }
        //
        // if (_setted)
        // {
        //     _setted = false;
        //     _previousPosition = Input.mousePosition;
        //     _startPosition = Input.mousePosition;
        //     return;
        // }

        var currentMousePosition = Input.mousePosition;

        var mouseDelta = (currentMousePosition - _previousPosition) * .032f;
        var toAdd = new Vector3(
            -mouseDelta.y,
            0,
            mouseDelta.x
        );
        transform.position += toAdd;
        // Debug.Log(mouseDelta + " - " + toAdd + " - " + transform.position);

        _previousPosition = Input.mousePosition;

        var ray = new Ray(hand.transform.position + Vector3.up * 3f, Vector3.down);

        if (Input.GetMouseButtonUp(0))
        {
            if (pointer)
            {
                pointer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                pointer = null;
                Dropped?.Invoke();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    Debug.Log("HIT! " + hit.collider.gameObject);
                    pointer = hit.collider.gameObject;
                    hit.rigidbody.constraints = RigidbodyConstraints.FreezeRotation |
                                                RigidbodyConstraints.FreezePositionY;

                    var rotation = pointer.transform.rotation.eulerAngles;
                    pointer.transform.rotation = Quaternion.Euler(0, rotation.y, 0);

                    Grabbed?.Invoke();
                }
                else if (hit.collider.GetComponent<Clickable>())
                {
                    hit.collider.GetComponent<Clickable>().OnClick();
                }
            }
        }
        else if (pointer)
        {
            var ray2 = new Ray(hand.transform.position + Vector3.up + Vector3.forward, Vector3.down);
            RaycastHit[] hits = Physics.RaycastAll(ray2, 50f);
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.collider.CompareTag("Desk") || hit.collider.CompareTag("TrashArea"))
                    {
                        pointer.transform.position = hit.point + Vector3.up * 2f;
                    }
                }
            }

            var rotateAmount = Input.mouseScrollDelta.y * 10f;
            pointer.transform.RotateAround(pointer.transform.position, Vector3.up, rotateAmount);
        }
        else if (Input.GetMouseButtonDown(1))
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
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    possiblePointer = hit.collider.gameObject;
                    Hovering?.Invoke();
                }
            }
        }
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

    //
    // [DllImport("user32.dll")]
    // static extern bool SetCursorPos(int X, int Y);
}