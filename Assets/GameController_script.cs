using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_script : MonoBehaviour
{
    public UnityEngine.UI.Text ScoreText; //текст счета
    public UnityEngine.UI.Text LevelText; //текст уровня оружия
    public UnityEngine.UI.Text XPText;
    public UnityEngine.UI.Text Text;

    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button recordButton;
    public UnityEngine.UI.Button exitButton;
    public UnityEngine.UI.Button exitButtonSettings;

    public UnityEngine.UI.Button Button_Easy;
    public UnityEngine.UI.Button Button_Normal;
    public UnityEngine.UI.Button Button_Hard;

    public UnityEngine.UI.Button Button_Resume_InGame;
    public UnityEngine.UI.Button Button_Settings_InGame;
    public UnityEngine.UI.Button Button_Exit_InGame;

    public UnityEngine.UI.Button Button_Pause;
    public UnityEngine.UI.Button Button_SelectLanguage;

    public GameObject menu;
    public GameObject settings;
    public GameObject difficult;
    public GameObject mainCamera;
    public GameObject InGameMenu;
    public GameObject Canvas;
    public GameObject InGameUI;
    public LeanLocalization leanLocalization;
    public GameObject MovingZone;

    private GameObject Player;
    private Player_script Player_Script; //объект скрипта игрока

    protected int Score = 0; //переменная кол-ва очков
    protected int ScoreForNextLevel = 300;
    private bool isStarted = false; //переменная начала игры
    protected int Mode = 0;
    protected bool SettingsMode = false;

    public bool getIsStarted()
    {
        return isStarted;
    }

    public int getMode()
    {
        return Mode;
    }

    public void IncreaseScore(string tag)
    {
        switch (tag)
        {
            case "LightEnemy":
                {
                    Score += 5;
                    break;
                }
            case "StandartEnemy":
                {
                    Score += 10;
                    break;
                }
            case "HardEnemy":
                {
                    Score += 15;
                    break;
                }
            case "Boss":
                {
                    Score += 100;
                    break;
                }
        }
        ScoreText.text = Score.ToString();

        if ((double)Score / (double)ScoreForNextLevel > 1)
        {
            Player_Script.IncreaseGunLevel();
            LevelText.text = Player_Script.GetGunLevel().ToString();
            ScoreForNextLevel *= 2;

            if (Player_Script.GetGunLevel() == 3)
            {
                Player_Script.shotDelay = 0.4F;
            }
            if (Player_Script.GetGunLevel() == 5)
            {
                Player_Script.shotDelay = 0.3F;
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Canvas.transform.position = new Vector3(0, 300, 0);
        InGameMenu.SetActive(false);
        mainCamera.SetActive(false);
        settings.SetActive(false);
        difficult.SetActive(false);
        InGameMenu.SetActive(false);
        InGameUI.SetActive(false);
        menu.SetActive(true);

        Player = GameObject.Find("Player");
        Player_Script = Player.GetComponent<Player_script>();

        startButton.onClick.AddListener(delegate
        {
            LevelText.text = Player_Script.GetGunLevel().ToString();
            ScoreText.text = Score.ToString();
            menu.SetActive(false); //выключение меню
            settings.SetActive(false);
            difficult.SetActive(true);
        });

        recordButton.onClick.AddListener(delegate
        {
            //records
        });

        exitButton.onClick.AddListener(delegate
        {
            Application.Quit(); //выход из приложения
        }
        );

        exitButtonSettings.onClick.AddListener(delegate
        {
            if (SettingsMode) //если во время игры
            {
                settings.SetActive(false);
                InGameMenu.SetActive(true);
                InGameUI.SetActive(true);
            }
            else //если в меню
            {
                settings.SetActive(false);
                menu.SetActive(true);
            }
        });

        Button_Easy.onClick.AddListener(delegate
        {
            InGameUI.SetActive(true);
            Mode = 1;
            isStarted = true; //начало игры
            Score = 0;
            difficult.SetActive(false);
            mainCamera.SetActive(true);
            SettingsMode = true;
        });

        Button_Normal.onClick.AddListener(delegate
        {
            InGameUI.SetActive(true);
            Mode = 2;
            isStarted = true; //начало игры
            Score = 0;
            difficult.SetActive(false);
            mainCamera.SetActive(true);
            SettingsMode = true;
        });

        Button_Hard.onClick.AddListener(delegate
        {
            InGameUI.SetActive(true);
            Mode = 3;
            isStarted = true; //начало игры
            Score = 0;
            difficult.SetActive(false);
            mainCamera.SetActive(true);

            SettingsMode = true;
        });

        Button_Pause.onClick.AddListener(delegate
        {
            isStarted = false;
            Player_Script.GetRigidbody().velocity = new Vector3(0, 0, 0);

            if (SettingsMode) //если во время игры
            {
                InGameMenu.SetActive(true);
                MovingZone.SetActive(false);
            }
            else //если в меню
            {
                menu.SetActive(false);
                difficult.SetActive(false);
                //record.SetActive(false);        TODO
                settings.SetActive(true);
            }
        });

        Button_Resume_InGame.onClick.AddListener(delegate
        {
            isStarted = true;
            MovingZone.SetActive(true);
            InGameMenu.SetActive(false);
        });

        Button_Settings_InGame.onClick.AddListener(delegate
        {
            settings.SetActive(true);
            InGameMenu.SetActive(false);
            InGameUI.SetActive(false);
        });

        Button_Exit_InGame.onClick.AddListener(delegate //выход в меню
        {
            menu.SetActive(true);
            InGameMenu.SetActive(false);
            isStarted = false;
            SettingsMode = false;
            mainCamera.SetActive(false);
            InGameUI.SetActive(false);
        });

        Button_SelectLanguage.onClick.AddListener(delegate
        {
            if (Button_SelectLanguage.GetComponentInChildren<Text>().text == "English")
            {
                leanLocalization.SetCurrentLanguage(1);
                Button_SelectLanguage.GetComponentInChildren<Text>().text = "Русский";
                ScoreText.rectTransform.localPosition = new Vector3(-49, 298, 0);
                LevelText.rectTransform.localPosition = new Vector3(98, 298, 0);
            }
            else
            {
                leanLocalization.SetCurrentLanguage(0);
                Button_SelectLanguage.GetComponentInChildren<Text>().text = "English";
                ScoreText.rectTransform.localPosition = new Vector3(-40, 298, 0);
                LevelText.rectTransform.localPosition = new Vector3(70, 298, 0);

            }
        });
    }

    void Update()
    {
        if (Player_Script.GetPlayerLife() <= 0)
        {
            isStarted = false;
            //end game
        }
        XPText.text = Player_Script.GetPlayerLife().ToString();
    }
}
