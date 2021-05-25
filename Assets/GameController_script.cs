using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController_script : MonoBehaviour
{
    public UnityEngine.UI.Text Data;
    public UnityEngine.UI.Text EndGameScoreText;

    public UnityEngine.UI.Button Button_StartGame;
    public UnityEngine.UI.Button Button_Records;
    public UnityEngine.UI.Button Button_Exit;
    public UnityEngine.UI.Button Button_ExitSettings;
    public UnityEngine.UI.Button Button_ExitRecords;

    public UnityEngine.UI.Button Button_Easy;
    public UnityEngine.UI.Button Button_Normal;
    public UnityEngine.UI.Button Button_Hard;

    public UnityEngine.UI.Button Button_Resume_InGame;
    public UnityEngine.UI.Button Button_Settings_InGame;
    public UnityEngine.UI.Button Button_Exit_InGame;

    public UnityEngine.UI.Button Button_Pause;
    public UnityEngine.UI.Button Button_SelectLanguage;
    public UnityEngine.UI.Button Button_End;

    public GameObject menu;
    public GameObject settings;
    public GameObject difficult;
    public GameObject records;
    public GameObject mainCamera;
    public GameObject InGameMenu;
    public GameObject Canvas;
    public GameObject InGameUI;
    public GameObject GameEnd;
    public LeanLocalization leanLocalization;

    private GameObject Player;
    private Player_script Player_Script; //объект скрипта игрока
    private EnemyCreator_script enemyCreator_Script;

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

        if ((double)Score / (double)ScoreForNextLevel > 1)
        {
            Player_Script.IncreaseGunLevel();
            ScoreForNextLevel *= 2;
            enemyCreator_Script.NextWaveDelay -= 3;
            enemyCreator_Script.AddHPForEnemies();

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
        records.SetActive(false);
        menu.SetActive(true);

        Player = GameObject.Find("Player");
        Player_Script = Player.GetComponent<Player_script>();
        enemyCreator_Script = GameObject.Find("EnemyCreators").GetComponent<EnemyCreator_script>();
        LoadRecords();

        if (PlayerPrefs.HasKey("SelectedLanguage"))
        {
            Language = PlayerPrefs.GetInt("SelectedLanguage");

            Debug.Log("Language loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }

        Button_StartGame.onClick.AddListener(delegate
        {
            menu.SetActive(false); //выключение меню
            settings.SetActive(false);
            difficult.SetActive(true);

            Player_Script.gameObject.SetActive(true);
            Player.transform.position = new Vector3(0, 0, -60);
        });

        Button_End.onClick.AddListener(delegate
        {
            SettingsMode = false;
            menu.SetActive(true);
            GameEnd.SetActive(false);
            InGameUI.SetActive(false);
            Button_Pause.enabled = true;

            isGameEnd = true;
        });

        Button_Records.onClick.AddListener(delegate
        {
            LoadRecords();

            menu.SetActive(false);
            records.SetActive(true);
        });

        Button_Exit.onClick.AddListener(delegate
        {
            Application.Quit(); //выход из приложения
        }
        );

        Button_ExitSettings.onClick.AddListener(delegate
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
            Player_Script.SetGunLevel(1);
            Player_Script.SetPlayerLife(10);

            enemyCreator_Script.SetDefaultSettings();

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
            Player_Script.SetGunLevel(1);
            Player_Script.SetPlayerLife(7);

            enemyCreator_Script.SetDefaultSettings();

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
            Player_Script.SetGunLevel(1);
            Player_Script.SetPlayerLife(5);

            enemyCreator_Script.SetDefaultSettings();

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
                records.SetActive(false);
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
            isGameEnd = true;
        });

        Button_SelectLanguage.onClick.AddListener(delegate
        {
            if (Button_SelectLanguage.GetComponentInChildren<Text>().text == "English")
            {
                Language = 1;
                PlayerPrefs.SetInt("SelectedLanguage", 1);
                leanLocalization.SetCurrentLanguage(1);
                Button_SelectLanguage.GetComponentInChildren<Text>().text = "Русский";
            }
            else
            {
                Language = 0;
                PlayerPrefs.SetInt("SelectedLanguage", 0);
                leanLocalization.SetCurrentLanguage(0);
                Button_SelectLanguage.GetComponentInChildren<Text>().text = "English";
            }
            PlayerPrefs.Save();
        });

        Button_ExitRecords.onClick.AddListener(delegate
        {
            records.SetActive(false);
            menu.SetActive(true);
        });
    }

    void Update()
    {
        if (isStarted)
        {
            if (Player_Script.GetPlayerLife() <= 0)//end game
            {
                isStarted = false;

                if (Language == 1)
                {
                    EndGameScoreText.text = "Счёт: " + Score.ToString();
                }
                else
                {
                    EndGameScoreText.text = "Score: " + Score.ToString();
                }
                SaveRecord();

                GameEnd.SetActive(true);
                Button_Pause.enabled = false;
            }
        }

        if (Language == 1) //ru
        {
            Data.text = string.Format($"Счёт:{Score}\t\t\tУровень:{Player_Script.GetGunLevel()}\t  ОЗ:{Player_Script.GetPlayerLife()}");
        }
        else //en
        {
            Data.text = string.Format($"Score:{Score}\t\t\tLevel:{Player_Script.GetGunLevel()}\t\tHP:{Player_Script.GetPlayerLife()}");
        }
    }

    private void SaveRecord()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Records.dat", FileMode.Append)))
        {
            Record record = new Record("Player", 0, Score);

            writer.Write(record.name);
            writer.Write(record.id);
            writer.Write(record.score);
        }
    }

    private void LoadRecords()
    {
        List<Record> RecordsList = new List<Record>();

        using (BinaryReader reader = new BinaryReader(File.Open(Application.persistentDataPath + "/Records.dat", FileMode.OpenOrCreate)))
        {
            while (reader.PeekChar() > -1)
            {
                RecordsList.Add(new Record(reader.ReadString(), reader.ReadInt32(), reader.ReadInt32())); //name id score
            }
        }

        RecordsList.Sort(delegate (Record x, Record y)
        {
            return y.score.CompareTo(x.score);
        });

        if (Language == 1)
        {
            records.transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").GetComponent<Text>().text
                = string.Format($"No\t\tИгрок \t\t\tСчёт \n");
        }
        else
        {
            records.transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").GetComponent<Text>().text
                = string.Format($"No\t\tPlayer\t\tScore\n");
        }


        for (int i = 0; i < RecordsList.Count; i++)
        {
            records.transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").GetComponent<Text>().text
                += string.Format($"{i}.\t\t{RecordsList[i].name}\t\t{RecordsList[i].score}\n");
        }
    }

    private void ResetRecords()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Records.dat", FileMode.Create)))
        {
            //
        }
    }
}
