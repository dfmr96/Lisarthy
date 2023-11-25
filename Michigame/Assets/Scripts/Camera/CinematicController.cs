using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour
{
    [SerializeField] private GameObject VirtualCamera;
    [SerializeField] private GameObject BossCameraCinematic;
    [SerializeField] private GameObject BossSprite;
    [SerializeField] private Transform[] points = new Transform[3];
    private Rigidbody2D rb2d;
    private float timer = 0;

    private int index = 0;
    private bool play = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = BossSprite.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerPrefs.DeleteKey("BossCinematic");
        timer += Time.deltaTime;
        if (play && timer > 1)
        {
            Animation();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("BossCinematic") == 1)
                return;
            
            PlayerPrefs.SetInt("BossCinematic", 1);
            BossSprite.SetActive(true);
            VirtualCamera.SetActive(false);
            BossCameraCinematic.SetActive(true);
            if (!play)
            {
                timer = 0;   
            }
            play = true;
        }
    }

    public void Animation()
    {
        Vector2 vec = points[index].position - BossSprite.transform.position;
        Vector2 norm = vec.normalized;
        rb2d.velocity = norm * 8;
        if (vec.magnitude < 1)
        {
            index++;
            if (index >= points.Length)
            {
                VirtualCamera.SetActive(true);
                BossCameraCinematic.SetActive(false);
                play = false;
                Destroy(BossSprite);
            }
        }

        
    }
}
