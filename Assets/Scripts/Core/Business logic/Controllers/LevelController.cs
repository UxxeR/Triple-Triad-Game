using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    /// <summary>
    /// Load an scene by name.
    /// </summary>
    /// <param name="level">Scene name.</param>
    public void LoadScene(string level)
    {
        SceneManager.LoadSceneAsync(level);
    }
}
