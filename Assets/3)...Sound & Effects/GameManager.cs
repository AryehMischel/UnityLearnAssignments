using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance { get { return _instance; } }
    
    private bool playing = true;
    private int _score = 0;
    private static GameManager _instance; 
    private UIManager uiManager;
    private PlayerController playerController;
    
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    
    void Start()
    {
        uiManager = UIManager.Instance;
        playerController = PlayerController.Instance;
    }

    public bool GameRunning()
    {
        return playing;
    }

    
    public void IncrementScore()
    {
        uiManager.UpdateScore(_score);
        _score++;
    }
    
    public void Reset()
    {
        playerController.Restart();
        playing = true;
    }
    
    public void endGame()
    {
        playing = false;
        Debug.Log("Game Over");

    }


}
