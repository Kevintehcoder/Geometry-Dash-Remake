using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelOne()
    {
        SceneManager.LoadScene(sceneName: "The Game");
    }

    public void LevelTwo()
    {
        if (Levelslscript.instance.CompletedLevelOne == true)
        {
            SceneManager.LoadScene(sceneName: "Level Two");
        }
    }
}
