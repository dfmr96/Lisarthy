using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public enum PlayerState
{
    Alive,
    Dead
}


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private AudioClip getdamage;
    [SerializeField] private AudioClip gethealth;

    [SerializeField] private LifeController life;

    [SerializeField] private PlayerState state;

    [SerializeField] private Vector2 lastCheckpoint;
    [SerializeField] private CameraController cameraController;

    public int maxHealth = 5;
    // Start is called before the first frame update

    private void Update()
    {
        Test();
    }

    private void Awake()
    {
        Debug.Log($"{PlayerPrefs.GetInt("CheckpointX")}, {PlayerPrefs.GetInt("CheckpointY")}");
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            lastCheckpoint.x = PlayerPrefs.GetInt("CheckpointX");
        }
        
        if (PlayerPrefs.HasKey("CheckpointY"))
        {
            lastCheckpoint.y = PlayerPrefs.GetInt("CheckpointY");
        }
        if (lastCheckpoint != Vector2.zero) transform.position = lastCheckpoint;
    }

    public void SetLastCheckpoint(Vector2 position)
    {
        lastCheckpoint = position;
        PlayerPrefs.SetInt("CheckpointX", (int)lastCheckpoint.x);
        PlayerPrefs.SetInt("CheckpointY", (int)lastCheckpoint.y);

        Debug.Log($"{PlayerPrefs.GetInt("CheckpointX")}, {PlayerPrefs.GetInt("CheckpointY")}");
            
    }
    public void TakeDamage(int damage)
    {

        AudioManager.Instance.PlaySound(getdamage);
        
        if (health >= 0)
        {
            
            health -= damage;
            life.UpdateHealth(health);
            
        }
        if (health <= 0)
        {
            ChangeState(PlayerState.Dead);
        }
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(11);
        }

    }
    public void TakeHealth(int heal)
    {
        
        if (health < maxHealth)
        {
            AudioManager.Instance.PlaySound(gethealth);
            health += heal;
            life.UpdateHealth(health);
        }
        
    }

    public void ChangeState(PlayerState newState)
    {
        state = newState;
        switch (state)
        {
            case PlayerState.Alive:
                break;
            case PlayerState.Dead:
                Debug.Log("Cambiar estado a muerto");
                ToggleMovement(false);
                gameObject.GetComponent<Animator>().SetTrigger("dead");
                StartCoroutine(Respawn());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ToggleMovement(bool active)
    {
        PlayerJump jump = GetComponent<PlayerJump>();
        PlayerMovement movement = GetComponent<PlayerMovement>();
        jump.enabled = active;
        movement.enabled = active;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        cameraController.Restart();
        ChangeState(PlayerState.Alive);
        transform.position = lastCheckpoint;
        health = maxHealth;
        life.Restart();
        gameObject.GetComponent<Animator>().SetTrigger("respawn");
        ToggleMovement(true);
        
    }

    public void IncreaseHealth()
    {
        maxHealth++;
    }
}
