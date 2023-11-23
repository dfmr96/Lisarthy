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

    private bool setBoss = false;

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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Tiles.SetActive(true);
            BossCamera.SetActive(true);
            VirtualCamera.SetActive(false);
            timer = 0;
            setBoss = true;
            
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
        
    }
}
