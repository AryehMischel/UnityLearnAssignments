using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    public TextMeshProUGUI scoreText;
    
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}