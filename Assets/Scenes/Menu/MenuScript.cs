using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private StyleSheet green_style;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("start").clicked += PlayMatch;
        //root.Q<Label>("highscore").text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0);
        root.Q<Label>("highscore").text = "Highscore: " + GameController.endscore;
        root.Q<Button>("exit").clicked += ExitGame;

        if(GameController.endscore != 0)
        {
            root.Q<Label>("highscore").styleSheets.Add(green_style);
        }
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
