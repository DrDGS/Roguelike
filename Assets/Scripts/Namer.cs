using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Namer : MonoBehaviour
{
    public int element;
    private string[] elemNames = { "No element", "Rock", "Paper", "Scissors" };
    [SerializeField] private TextMeshProUGUI elementbar;

    void Start()
    {
        element = Random.Range(1, 4); 
        elementbar.text = elemNames[element];   
    }
}
