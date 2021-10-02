using UnityEngine;

public class ConstructionPhase : MonoBehaviour
{
    public GameObject[] gridObjects;
    private GridObject _currentGridObject;
    private Pointer _pointer;
    private int _currentGridObjectIndex = 0;
    private GameManager _gameManager;

    void Start()
    {
        _pointer = FindObjectOfType<Pointer>();
        _gameManager = FindObjectOfType<GameManager>();
        
        DontDestroyOnLoad(gameObject);
    }

    public void NextGridObject()
    {   
        if (_currentGridObjectIndex >= gridObjects.Length)
        {
            Debug.LogError("Construction phase has no more grid objects to place");
            _gameManager.GoToPlanningPhase();
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

    public void StartPhase()
    {
        NextGridObject();
    }
}
