using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionPhase : MonoBehaviour
{
    public GameObject[] gridObjects;
    private GridObject _currentGridObject;
    private Pointer _pointer;
    private int _currentGridObjectIndex = 0;
    private GameManager _gameManager;
    private GameObject _worldScene;

    void Start()
    {
        _pointer = FindObjectOfType<Pointer>();
        _gameManager = FindObjectOfType<GameManager>();
        _worldScene = FindObjectOfType<WorldScene>().gameObject;
        
        DontDestroyOnLoad(gameObject);
    }

    public void NextGridObject()
    {
        StartCoroutine(DoSoon());
        
        IEnumerator DoSoon()
        {
            yield return new WaitForSeconds(.1f);
            
            if (_currentGridObjectIndex >= gridObjects.Length)
            {
                Debug.LogError("Construction phase has no more grid objects to place");
                _gameManager.GoToPlanningPhase();
            }
            else
            {
                var nextTemplate = gridObjects[_currentGridObjectIndex++];
                var next = Instantiate(nextTemplate,  _worldScene.transform);
            
                _pointer.pointer = next;
                _currentGridObject = next.GetComponent<GridObject>();
                _currentGridObject.OnBuild += NextGridObject;
            }
        }
    }

    public void StartPhase()
    {
        _currentGridObjectIndex = 0;
        NextGridObject();
    }
}
