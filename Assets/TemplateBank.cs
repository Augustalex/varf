using System;
using UnityEngine;

public class TemplateBank : MonoBehaviour
{
    public GameObject houseTemplate;
    public GameObject house90DegTemplate;
    public GameObject boatTemplate;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}