
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void GoToNextLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
