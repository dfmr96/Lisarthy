using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private float fireRate;
    private float ViewRange;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D viewRangeR = Physics2D.Raycast(transform.position, transform.right, ViewRange, player.layer);
        RaycastHit2D viewRangeL = Physics2D.Raycast(transform.position, transform.right * -1, ViewRange, player.layer);

        if (viewRangeR)
        {
            Instantiate(bullet,transform.position, transform.rotation);
        }
    }
}
