using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private DataSaver data;
    public void RestartGame()
    {
        data.Score = 0;
        data.Level = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
