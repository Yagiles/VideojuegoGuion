using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Escena 0(Puerta)");
    }

    public void QuitGame()
    {
        Application.Quit();
        // En el editor no cierra, esto es solo para build
    }
}