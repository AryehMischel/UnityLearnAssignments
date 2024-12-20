using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20;
    private GameManager gameManager;
    private float leftBound = -15f;
    void Start()
    {
        gameManager = GameManager.Instance;
     
    }
    
    void FixedUpdate()
    {
        if (gameManager.GameRunning())
        {
            transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }
        
        if(transform.position.x < leftBound && (gameObject.CompareTag("Obstacle") || gameObject.CompareTag("Collectable")))
        {
            Destroy(gameObject);
        }
        
    }
}
