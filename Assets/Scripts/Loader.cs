using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    /// <summary>
    /// This Reloads the scene.
    /// </summary>
    public void Reload()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
