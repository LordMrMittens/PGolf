using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadNewScene()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.ResetGame();
    }
}
