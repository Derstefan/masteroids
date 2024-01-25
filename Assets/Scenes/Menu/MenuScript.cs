using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuScript : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("start").clicked += PlayMatch;
        root.Q<Label>("highscore").text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0);
        root.Q<Button>("exit").clicked += ExitGame;


    }

    private void PlayMatch()
    {

        // Load the Game scene
        SceneManager.LoadScene("Game");
    }



    private void ExitGame()
    {

        // Quit the application
        Application.Quit();
    }
}
