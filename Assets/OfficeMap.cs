using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeMap : Clickable
{
    public override void OnClick()
    {
        var gameManager = FindObjectOfType<GameManager >();
        gameManager.GoToConstructionPhase();
    }
}
