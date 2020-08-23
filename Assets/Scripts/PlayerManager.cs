using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// opreste jocul, apare panel ul de gameOver

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject GameOverPanel;

    public static int moneyNumber;
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1; // cand e gameover, Time.timeScale devine 0
        moneyNumber = 0;
    }

    // Update is called once per frame
    public Text moneyText;
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }

        moneyText.text = "FuelMoney: " + moneyNumber; // pt ca banii colectati sa apara pe ecran
    }
}
