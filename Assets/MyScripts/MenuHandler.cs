﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class MenuHandler : MonoBehaviour {

    private bool signedIn = false;
    private bool attemptedToSignIn = false;

    public Text signIn;

    public Button gameMode1;
    public Button gameMode2;
    public Button gameMode3;

    public Text highScore_1;
    public Text highScore_2;
    public Text highScore_3;

	// Use this for initialization
	void Start () {
        attemptedToSignIn = PlayerPrefs.GetInt("AttemptedToSignIn", 0) == 1;
        signedIn = PlayerPrefs.GetInt("leaderBoard", 0) == 1;
        bool idc = true;
        if (idc)
        {
            

            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            signIn.text = "";
            PlayerPrefs.SetInt("leaderBoard", 0);
            PlayerPrefs.SetInt("AttemptedToSignIn", 1);
            Social.localUser.Authenticate((bool sucess) =>
            {
                if (sucess)
                {
                    //signIn.text = "Signed In";
                    Debug.Log("You've sucessfully logged in!");
                    PlayerPrefs.SetInt("leaderBoard", 1);
                }
                else
                {
                    //signIn.text = "Failed";
                    Debug.Log("Failed to log in");
                    PlayerPrefs.SetInt("leaderBoard", 0);
                }
            });

        }
        three_by_three();
	}
	
    public void three_by_three()
    {
        PlayerPrefs.SetString("current_level", "3x3");

        gameMode1.GetComponentInChildren<Text>().text = "30 Seconds";
        gameMode2.GetComponentInChildren<Text>().text = "1 Minute";
        gameMode3.GetComponentInChildren<Text>().text = "2 Minutes";

        highScore_1.text = PlayerPrefs.GetInt("3x3_30", 0).ToString();
        highScore_2.text = PlayerPrefs.GetInt("3x3_60", 0).ToString();
        highScore_3.text = PlayerPrefs.GetInt("3x3_120", 0).ToString();
    }

    public void four_by_four()
    {
        PlayerPrefs.SetString("current_level", "4x4");

        gameMode1.GetComponentInChildren<Text>().text = "1 Minute";
        gameMode2.GetComponentInChildren<Text>().text = "2 Minutes";
        gameMode3.GetComponentInChildren<Text>().text = "3 Minutes";

        highScore_1.text = PlayerPrefs.GetInt("4x4_60", 0).ToString();
        highScore_2.text = PlayerPrefs.GetInt("4x4_120", 0).ToString();
        highScore_3.text = PlayerPrefs.GetInt("4x4_180", 0).ToString();
    }

    public void loadLevel_with_time()
    {
        switch (PlayerPrefs.GetString("current_level"))
        {
            case "3x3":
                Application.LoadLevel("3x3");
                break;
            case "4x4":
                Application.LoadLevel("4x4");
                break;
        }
    }

    public void selectMode(int buttonNumber)
    {
        Debug.Log(PlayerPrefs.GetString("current_level"));
        switch (buttonNumber)
        {
            case 1:
                if(PlayerPrefs.GetString("current_level") == "3x3")
                {
                    PlayerPrefs.SetFloat("time", 30);
                }
                else
                {
                    PlayerPrefs.SetFloat("time", 60);
                }
                break;
            case 2:
                if (PlayerPrefs.GetString("current_level") == "3x3")
                {
                    PlayerPrefs.SetFloat("time", 60);
                }
                else
                {
                    PlayerPrefs.SetFloat("time", 120);
                }
                break;
            case 3:
                if (PlayerPrefs.GetString("current_level") == "3x3")
                {
                    PlayerPrefs.SetFloat("time", 120);
                }
                else
                {
                    PlayerPrefs.SetFloat("time", 180);
                }
                break;
        }
        loadLevel_with_time();
    }

    public void show_leaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
}
