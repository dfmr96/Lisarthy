using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public void Close()
    {
       gameObject.GetComponentInParent<GameObject>().SetActive(false);
    }
    
    public void UnPause()
    {
        Time.timeScale = 1.0f;
    }
}
