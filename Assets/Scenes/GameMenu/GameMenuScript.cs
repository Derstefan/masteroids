using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuScript : MonoBehaviour
{
    private GameController gameController;
    private Label ui_highscore;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        ui_highscore = root.Q<Label>("score");
    }

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        SetHighScore();
    }

    public void SetHighScore()
    {
        ui_highscore.text = $"SCORE: {gameController.highscore}";
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
