using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuScript : MonoBehaviour
{
    private GameController gameController;
    private Label HighscoreUI;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        HighscoreUI = root.Q<Label>("score");
    }

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        SetHighScore();
    }

    public void SetHighScore()
    {
        HighscoreUI.text = $"SCORE: {gameController.highscore}";
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
