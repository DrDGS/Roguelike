using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] private DataSaver data;
    public void RestartGame()
    {
        data.Score = 0;
        data.Level = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
