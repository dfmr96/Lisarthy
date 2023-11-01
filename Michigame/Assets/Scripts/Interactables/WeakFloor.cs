using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakFloor : MonoBehaviour, ITailinteractionable
{  

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TailPunchAction()
    {
        Destroy(this.gameObject);
    }
}
