using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuScript : MonoBehaviour
{
    private GameController gameController;
    private Label HighscoreUI;
    private VisualElement ProgressBar;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        HighscoreUI = root.Q<Label>("score");
        ProgressBar = root.Q<VisualElement>("Foreground");
        root.Q<Button>("menu").clicked += LoadMenu;
    }

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();        
    }

    public void SetHighScore(Component sender, object data)
    {
        if(data is int)
        {
            HighscoreUI.text = $"SCORE: {(int) data}";
        }
    }

    public void SetProgress(Component sender, object data)
    {
        if(data is float)
        {
            ProgressBar.transform.scale = new Vector3((float) data, 1, 1);
        }
        
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
