using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatbackground : MonoBehaviour
{

    private Vector3 startPosition;

    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        repeatWidth = 112.8f / 2.0f;  //GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPosition.x - repeatWidth)
        {
            transform.position = startPosition;
        }
    }
}
