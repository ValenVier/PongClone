using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BallController ball;
    [SerializeField] private Paddle player1Paddle;
    [SerializeField] private Paddle player2Paddle;

    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;
    [SerializeField] private TextMeshProUGUI player1Name;
    [SerializeField] private TextMeshProUGUI player2Name;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;

    private int player1Score;
    private string p1Name;
    private int player2Score;
    private string p2Name;
    private int maxScore = 5;
    private bool gameEnded = false;

    private void Start()
    {
        p1Name = PlayerPrefs.GetString("Player1Name", "Player1");
        p2Name = PlayerPrefs.GetString("Player2Name", "Player2");
        SetPlayerNames(p1Name, p2Name);
        
        NewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        gameEnded = false;
        gameOverText.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        
        SetPlayer1Score(0);
        SetPlayer2Score(0);
        NewRound();
    }

    public void NewRound()
    {
        if (gameEnded) return;
        
        player1Paddle.ResetPosition();
        player2Paddle.ResetPosition();
        ball.ResetPosition();

        CancelInvoke();
        Invoke(nameof(StartRound), 1f);
    }

    private void StartRound()
    {
        if (gameEnded) return;
        ball.AddStartingForce();
    }

    public void OnPlayer1Scored()
    {
        if (gameEnded) return;
        
        SetPlayer1Score(player1Score + 1);
        CheckGameEnd();
        NewRound();
    }

    public void OnPlayer2Scored()
    {
        if (gameEnded) return;
        
        SetPlayer2Score(player2Score + 1);
        CheckGameEnd();
        NewRound();
    }

    private void SetPlayer1Score(int score)
    {
        player1Score = score;
        player1ScoreText.text = score.ToString();
    }

    private void SetPlayer2Score(int score)
    {
        player2Score = score;
        player2ScoreText.text = score.ToString();
    }
    
    private void CheckGameEnd()
    {
        if (player1Score >= maxScore || player2Score >= maxScore)
        {
            gameEnded = true;

            string winner = player1Score > player2Score ? p1Name + " Wins!" : p2Name + " Wins!";
            gameOverText.text = winner;
            gameOverText.gameObject.SetActive(true);
            gameOverPanel.gameObject.SetActive(true);

            // Stop the movement of the ball
            ball.SetVelocity(new Vec3(0f, 0f, 0f));
        }
    }
    
    public void SetPlayerNames(string p1, string p2)
    {
        player1Name.text = p1;
        player2Name.text = p2;
    }
    
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
