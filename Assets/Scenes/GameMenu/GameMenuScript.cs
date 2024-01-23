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
        root.Q<Button>("menu").clicked += LoadMenu;
    }

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        //SetHighScore();
    }

    public void SetHighScore(Component sender, object data)
    {
        if(data is int)
        {
            HighscoreUI.text = $"SCORE: {(int) data}";
        }
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
