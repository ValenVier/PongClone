using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public TMP_InputField player1Input;
    public TMP_InputField player2Input;

    public void OnStartButton()
    {
        PlayerPrefs.SetString("Player1Name", string.IsNullOrEmpty(player1Input.text) ? "Player1" : player1Input.text);
        PlayerPrefs.SetString("Player2Name", string.IsNullOrEmpty(player2Input.text) ? "Player2" : player2Input.text);

        SceneManager.LoadScene("Pong");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}