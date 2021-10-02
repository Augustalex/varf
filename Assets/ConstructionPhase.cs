using UnityEngine;

public class ConstructionPhase : MonoBehaviour
{
    public GameObject[] gridObjects;
    private GridObject _currentGridObject;
    private Pointer _pointer;
    private int _currentGridObjectIndex = 0;

    void Start()
    {
        _pointer = FindObjectOfType<Pointer>();
        NextGridObject();
    }

    public void NextGridObject()
    {   
        if (_currentGridObjectIndex >= gridObjects.Length)
        {
            Debug.LogError("Construction phase has no more grid objects to place");
        }
        else
        {
            var nextTemplate = gridObjects[_currentGridObjectIndex++];
            var next = Instantiate(nextTemplate);
            
            Debug.Log(next);
            _pointer.pointer = next;
            _currentGridObject = next.GetComponent<GridObject>();
            _currentGridObject.OnBuild += NextGridObject;
        }
    }
}
