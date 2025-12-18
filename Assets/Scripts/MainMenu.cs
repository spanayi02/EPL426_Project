using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public TMP_Text highScoreUI;

    string newGameScene = "fps";

    public AudioClip bg_music;
    public AudioSource main_channel;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        main_channel.PlayOneShot(bg_music);
        // Set the high score text
        int highScore = SaveLoadManager.Instance.LoadHighScore();
        highScoreUI.text = $"Top Wave Survived: {highScore}";
    }

    public void StartNewGame()
    {
        Time.timeScale = 1f;
        main_channel.Stop();
        SceneManager.LoadScene(newGameScene);
    }
    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 
    }
}