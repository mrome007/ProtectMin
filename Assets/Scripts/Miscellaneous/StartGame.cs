using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour 
{
    private void Start()
    {
        Invoke("StartTheGame", 10f);
    }

    private void StartTheGame()
    {
        SceneManager.LoadScene(1);
    }
}
