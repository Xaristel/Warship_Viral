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
    public UnityEngine.UI.Text EndGameScoreText;

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
    public UnityEngine.UI.Button EndButton;

    public GameObject menu;
    public GameObject settings;
    public GameObject difficult;
    public GameObject mainCamera;
    public GameObject InGameMenu;
    public GameObject Canvas;
    public GameObject InGameUI;
    public GameObject GameEnd;
    public LeanLocalization leanLocalization;

    private GameObject Player;
    private Player_script Player_Script; //объект скрипта игрока

    protected int Score = 0; //переменная кол-ва очков
    protected int ScoreForNextLevel = 300;
    private bool isStarted = false; //переменная начала игры
    protected int Mode = 0;
    protected bool SettingsMode = false;
    private int Language = 0;
    private bool isGameEnd = false;

    public bool getIsStarted()
    {
        return isStarted;
    }

    public bool GetIsGameEnd()
    {
        return isGameEnd;
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

        if (PlayerPrefs.HasKey("SelectedLanguage"))
        {
            Language = PlayerPrefs.GetInt("SelectedLanguage");

            Debug.Log("Language loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }

        if (Language == 1)
        {
            ScoreText.rectTransform.localPosition = new Vector3(-50, 298, 0);
            LevelText.rectTransform.localPosition = new Vector3(51, 298, 0);
        }
        else
        {
            ScoreText.rectTransform.localPosition = new Vector3(-44, 298, 0);
            LevelText.rectTransform.localPosition = new Vector3(26, 298, 0);
        }

        startButton.onClick.AddListener(delegate
        {
            menu.SetActive(false); //выключение меню
            settings.SetActive(false);
            difficult.SetActive(true);

            Player_Script.gameObject.SetActive(true);
            Player.transform.position = new Vector3(0, 0, -60);
            Player_Script.SetPlayerLife(10);
        });

        EndButton.onClick.AddListener(delegate
        {
            SettingsMode = false;
            menu.SetActive(true);
            GameEnd.SetActive(false);
            InGameUI.SetActive(false);
            Button_Pause.enabled = true;

            isGameEnd = true;
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
            Mode = 1;
            Score = 0;
            ScoreForNextLevel = 300;
            ScoreText.text = Score.ToString();
            LevelText.text = Player_Script.GetGunLevel().ToString();
            EndGameScoreText.text = "";

            InGameUI.SetActive(true);
            difficult.SetActive(false);
            mainCamera.SetActive(true);

            isStarted = true; //начало игры
            SettingsMode = true;
            isGameEnd = false;
        });

        Button_Normal.onClick.AddListener(delegate
        {
            Mode = 2;
            Score = 0;
            ScoreForNextLevel = 300;
            ScoreText.text = Score.ToString();
            LevelText.text = Player_Script.GetGunLevel().ToString();
            EndGameScoreText.text = "";

            InGameUI.SetActive(true);
            difficult.SetActive(false);
            mainCamera.SetActive(true);

            isStarted = true; //начало игры
            SettingsMode = true;
            isGameEnd = false;
        });

        Button_Hard.onClick.AddListener(delegate
        {
            Mode = 3;
            Score = 0;
            ScoreForNextLevel = 300;
            ScoreText.text = Score.ToString();
            LevelText.text = Player_Script.GetGunLevel().ToString();
            EndGameScoreText.text = "";

            InGameUI.SetActive(true);
            difficult.SetActive(false);
            mainCamera.SetActive(true);

            isStarted = true; //начало игры
            SettingsMode = true;
            isGameEnd = false;
        });

        Button_Pause.onClick.AddListener(delegate
        {
            isStarted = false;
            if (Player != null)
            {
                Player_Script.GetRigidbody().velocity = new Vector3(0, 0, 0);
            }

            if (SettingsMode) //если во время игры
            {
                InGameMenu.SetActive(true);
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
                PlayerPrefs.SetInt("SelectedLanguage", 1);
                leanLocalization.SetCurrentLanguage(1);
                Button_SelectLanguage.GetComponentInChildren<Text>().text = "Русский";
                ScoreText.rectTransform.localPosition = new Vector3(-50, 298, 0);
                LevelText.rectTransform.localPosition = new Vector3(51, 298, 0);
            }
            else
            {
                PlayerPrefs.SetInt("SelectedLanguage", 0);
                leanLocalization.SetCurrentLanguage(0);
                Button_SelectLanguage.GetComponentInChildren<Text>().text = "English";
                ScoreText.rectTransform.localPosition = new Vector3(-44, 298, 0);
                LevelText.rectTransform.localPosition = new Vector3(26, 298, 0);
            }
            PlayerPrefs.Save();
        });
    }

    void Update()
    {
        if (isStarted)
        {
            if (Player_Script.GetPlayerLife() <= 0)//end game
            {
                isStarted = false;
                XPText.text = "0";

                if (Language == 1)
                {
                    EndGameScoreText.text = "Счёт: " + Score.ToString();
                }
                else
                {
                    EndGameScoreText.text = "Score: " + Score.ToString();
                }

                GameEnd.SetActive(true);
                Button_Pause.enabled = false;
            }
            XPText.text = Player_Script.GetPlayerLife().ToString();
        }

    }
}
