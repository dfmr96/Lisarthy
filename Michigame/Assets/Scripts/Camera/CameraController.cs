using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject Tiles;
    [SerializeField] private GameObject VirtualCamera;
    [SerializeField] private GameObject BossCamera;
    [SerializeField] private GameObject BossContainer;
    [SerializeField] private GameObject BossPrefab;
    [SerializeField] private GameObject ActualBoss;
    [SerializeField] private AudioClip MusicaMapa;
    [SerializeField] private AudioClip MusicaBoss;
    [SerializeField] private GameObject Flor;
    [SerializeField] private GameObject map;

    private bool setBoss = false;
    private bool flor = false;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5 && setBoss)
        {
            
            BossContainer.SetActive(true);
            ActualBoss.SetActive(true);
        }

        if (timer > 5.8 && flor)
        {
            Flor.SetActive(false);
            flor = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Tiles.SetActive(true);
            BossCamera.SetActive(true);
            VirtualCamera.SetActive(false);
            if (!setBoss)
            {
                map.SetActive(false);
                timer = 0;
                setBoss = true;
                flor = true;
                MusicManager.Instance.PlaySound(MusicaBoss);
            }
            
            
        }
    }

    public void Restart()
    {
        
        Tiles.SetActive(false);
        VirtualCamera.SetActive(true);
        BossCamera.SetActive(false);
        timer = 0;
        setBoss = false;
        Destroy(ActualBoss);
        ActualBoss = Instantiate(BossPrefab, BossContainer.transform);
        ActualBoss.SetActive(true);
        BossContainer.SetActive(false);
        MusicManager.Instance.PlaySound(MusicaMapa);
        flor = true;
        Flor.SetActive(true);
        map.SetActive(true);

    }
}
