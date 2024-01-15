using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuScript : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("menu").clicked += LoadMenu;
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
