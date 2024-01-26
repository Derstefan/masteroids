using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuScript : MonoBehaviour
{
    //VisualElements
    private VisualElement HUD;
    private VisualElement levelMenu;
    private Label highscoreUI;
    private VisualElement progressBar;
    private VisualElement healthBar;
    private Button weaponImage;

    private List<Button> buttons = new List<Button>();
    private List<String> selectableSkills = new List<string>();

    [Header("Events")]
    public GameEvent OnSkillSelected;

    public AudioClip skillSound;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        SetUpHUD(root);
        SetUpLevelMenu(root);
    }

    private void SetUpHUD(VisualElement root)
    {
        HUD = root.Q<VisualElement>("HUD");
        highscoreUI = root.Q<Label>("score");
        progressBar = root.Q<VisualElement>("Foreground");
        healthBar = root.Q<VisualElement>("Foreground_health");
        weaponImage = root.Q<Button>("Weapon_Image");
        root.Q<Button>("menu").clicked += LoadMenu;
    }

    private void SetUpLevelMenu(VisualElement root)
    {
        levelMenu = root.Q<VisualElement>("LevelUp_Menu");

        foreach (VisualElement element in levelMenu.hierarchy.Children())
        {
            if (element is Button)
            {
                buttons.Add((Button)element);
            }
        }

        foreach (Button button in buttons)
        {
            button.RegisterCallback<ClickEvent>(GetSkillFromButton);
        }

        SetLevelMenuInactive();
    }

    private void GetSkillFromButton(ClickEvent evt)
    {
        AudioSource.PlayClipAtPoint(skillSound, Camera.main.transform.position);
        Button selectedButton = evt.currentTarget as Button;
        int buttonIndex = buttons.IndexOf(selectedButton);
        string skill = selectableSkills[buttonIndex];
        //string skill = selectedButton.text;
        OnSkillSelected.Raise(this, skill);
        selectableSkills.Clear();
        SetLevelMenuInactive();
    }

    public void SetHighScore(Component sender, object data)
    {
        if (data is int)
        {
            highscoreUI.text = $"SCORE: {(int)data}";
        }
    }

    public void SetProgress(Component sender, object data)
    {
        if (data is float)
        {
            progressBar.transform.scale = new Vector3((float)data, 1, 1);
        }
    }

    public void SetHealth(Component sender, object data)
    {
        if (data is float)
        {
            healthBar.transform.scale = new Vector3((float)data, 1, 1);
        }
    }

    public void SetLevelMenuActive(Component sender, object data)
    {
        if (data is Skill[])
        {
            //Show skill selection
            Skill[] weapons = (Skill[])data;
            SetupSelectionMenu(weapons);
            levelMenu.style.display = DisplayStyle.Flex;

            //hide HUD
            foreach (VisualElement element in HUD.hierarchy.Children())
            {
                element.style.display = DisplayStyle.None;
            }
        }
    }

    private void SetupSelectionMenu(Skill[] weapons)
    {
        int i = 0;

        foreach (Button button in buttons)
        {
            if (i < weapons.Length)
            {
                selectableSkills.Add(weapons[i].name);
                String buttonText = RemoveVovels(weapons[i].name);
                button.text = buttonText;
                button.style.display = DisplayStyle.Flex;
                button[0].style.backgroundImage = new StyleBackground(weapons[i].sprite);
                i++;
            }
        }
    }

    private String RemoveVovels(string name)
    {
        name = name.ToUpper();
        String strg = "";
        foreach (char c in name)
        {
            if (!IsVovel(c))
            {
                strg += c;
            }
        }
        return strg;
    }

    private bool IsVovel(char c)
    {
        return (c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U');
    }

    public void SetLevelMenuInactive()
    {
        // hide skill selection
        foreach (Button button in buttons)
        {
            button.style.display = DisplayStyle.None;
        }

        levelMenu.style.display = DisplayStyle.None;

        // show HUD
        foreach (VisualElement element in HUD.hierarchy.Children())
        {
            element.style.display = DisplayStyle.Flex;
        }
    }

    public void SetCurrentWeaponImage(Component sender, object data)
    {
        if (data is Sprite)
        {
            Sprite weaponSprite = (Sprite)data;
            weaponImage.style.backgroundImage = new StyleBackground(weaponSprite);
        }
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
