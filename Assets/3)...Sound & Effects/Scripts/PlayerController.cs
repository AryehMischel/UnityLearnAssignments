using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public bool gameOver = false;
    public bool isGrounded;
    private Animator anime;
    private GameObject canvas;
    void Awake()
    {
        Ground();
        controls = new PlayerControls();
        rb = this.GetComponent<Rigidbody>(); 
        controls.Player.Jump.started += _ => Jump();
        anime =  GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        canvas = GameObject.Find("Canvas");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            explosionParticle.Play();
            dirtParticle.Stop();
            gameOver = true;
            anime.SetTrigger("Death");

            playerAudio.PlayOneShot(crashSound, 1.0f);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            canvas.SetActive(true);
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            Ground();
        }
     
    }

    public void Ground()
    {
        if (gameOver == false)
        {
            isGrounded = true;
            dirtParticle.Play();
        }
      
    }

    private void Jump()
    {
        if (!gameOver)
        {
            if (isGrounded)
            {
                  
                isGrounded = false;
                if (!isGrounded)
                {
                    anime.SetTrigger("Jump_trig");

                }
                playerAudio.PlayOneShot(jumpSound, 1.0f); 
                dirtParticle.Stop();
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            
        }
      
      
    }

    public void Restart()
    {
        gameOver = false;
        Ground();
    }


    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();
}
