using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNav : MonoBehaviour
{
    public Canvas _currentCanvas;
    public Canvas _nextCanvas;
    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void resume()
    {
        Time.timeScale = 1;
        _currentCanvas.gameObject.SetActive(false);
    }

    public void exitApp()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void exitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void tutorial()
    {
        _currentCanvas.gameObject.SetActive(false);
        _nextCanvas.gameObject.SetActive(true);
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void returnToMenu()
    {
        _nextCanvas.gameObject.SetActive(false);
        _currentCanvas.gameObject.SetActive(true);
    }
}
