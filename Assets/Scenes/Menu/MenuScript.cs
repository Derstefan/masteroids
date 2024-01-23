using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuScript : MonoBehaviour
{

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("start").clicked += playMatch;
        root.Q<Button>("options").clicked += generatorScene;
        root.Q<Button>("exit").clicked += exitGame;
    }   

    private void playMatch()
    {

        SceneManager.LoadScene("Game");
    }

    private void generatorScene()
    {
        SceneManager.LoadScene("Game");
    }

    private void exitGame()
    {
        Application.Quit();
    }    
}
