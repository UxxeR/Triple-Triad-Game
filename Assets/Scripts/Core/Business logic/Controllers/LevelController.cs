using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public void LoadScene(string level)
    {
        SceneManager.LoadSceneAsync(level);
    }
}
