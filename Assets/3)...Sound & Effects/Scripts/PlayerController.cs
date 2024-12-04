using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody _rigidBody;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem powderParticle;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip sniff;
    public bool isGrounded;
    private Animator _animator;
    private GameObject canvas;
    private GameManager gameManager;
    private bool characterAlive = true;


    private static PlayerController _instance; 
    public static PlayerController Instance { get { return _instance; } }
    
    
    private void Awake()
    {
        
        
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
       
        controls = new PlayerControls();
        _rigidBody = this.GetComponent<Rigidbody>(); 
        controls.Player.Jump.started += _ => Jump();
        _animator =  GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    private void Start()
    {
        
       gameManager = GameManager.Instance;
       Ground();
    }

    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            
            characterAlive = false;
            gameManager.endGame();
            animateCharacterDeath();
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Ground();
        }
        
        else if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            playerAudio.PlayOneShot(sniff);
            powderParticle.transform.localPosition = transform.InverseTransformPoint(collision.contacts[0].point);
            powderParticle.Play();
            
            gameManager.IncrementScore();
        }
     
    }

    
    private void animateCharacterDeath()
    {
        _animator.SetTrigger("Death");

    }
    private void Ground()
    {
        if (gameManager.GameRunning())
        {
            isGrounded = true;
            dirtParticle.Play();
        }
      
    }

    private void Jump()
    {
        if (characterAlive && isGrounded)
        {
                isGrounded = false;
                _animator.SetTrigger("Jump_trig");
                playerAudio.PlayOneShot(jumpSound, 1.0f); 
                dirtParticle.Stop();
                _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
        }
      
      
    }

    public void Restart()
    {
        _animator.SetBool("Death_b", false);
        _animator.SetBool("Revive", true);
        isGrounded = true;
        characterAlive = true;
        StartCoroutine(Revival());
    }
    
    
    private IEnumerator Revival()
    {
        yield return new WaitForSeconds(1);
        _animator.SetBool("Revive", false);

    }


    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();
}
