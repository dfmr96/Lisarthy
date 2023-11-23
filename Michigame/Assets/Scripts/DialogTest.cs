using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    [SerializeField] GameObject playerDialogue;
    [SerializeField] GameObject npcDialogue;
    // Start is called before the first frame update
    void Start()
    {        
        npcDialogue.GetComponent<SpriteRenderer>().sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
