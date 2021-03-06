﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsBar : MonoBehaviour
{
    [Header("Hearts images")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Lives")]
    public int health; //- здоровье = заполненные сердечки
    public int heartsNumber; //количество ВИДИМЫХ сердечек на канвасе (заполненных и незаполненных)
    [SerializeField] private int startHeartsNumber;

    [Header("Ball")]
    public Ball ball;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    GameManager gm;
    public Text totalScore;
    public bool isDead;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        for (int j = 0; j < hearts.Length; j++) //т.к. всего сердец 5, но видно только 3 в начале игры, надо их сделать НЕвидимыми все
        {
            hearts[j].enabled = false;
        }
        HeartsStart();
    }

    public void MinusHeart()
    {
        health--;
        for (int i = 0; i < heartsNumber; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < heartsNumber)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        GameOver();
    }
    public void AddRemoveHeart(int addHeart)
    {
        if (addHeart > 0)
        {
            if (health < heartsNumber)
            {
                hearts[heartsNumber - 1].sprite = fullHeart;
                health++;
            }
            else if (health == heartsNumber)
            {
                heartsNumber++;
                hearts[heartsNumber - 1].enabled = true;
                health++;
            }
        }
        else
        {
            MinusHeart();
        }

    }
    public void HeartsStart()
    {
        isDead = false;
        gm.isPaused = false;
        gm.score = 0;
        Time.timeScale = 1f;
        heartsNumber = startHeartsNumber;
        health = startHeartsNumber;
        for (int k = 0; k < heartsNumber; k++) //а после этого сделать видимыми только нужное количество (указывается в инспекторе)
        {
            hearts[k].sprite = fullHeart;
            hearts[k].enabled = true;
            hearts[k + 1].enabled = false;
            hearts[k + 2].enabled = false;
        }
    }

    public void GameOver()
    {
        if (health <= 0)
        {
            isDead = true;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            totalScore.text = "Total score: " + gm.score.ToString();
        }
    }
}
