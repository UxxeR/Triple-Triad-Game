using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Load an scene by name.
    /// </summary>
    /// <param name="level">Scene name.</param>
    public void LoadScene(string level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    /// <summary>
    /// Exit form the application.
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
