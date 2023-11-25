using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public float cinematicTime;
    public int scene;
    private void Start()
    {
        StartCoroutine(ChangeScene());
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(cinematicTime);
        SceneManager.LoadScene(scene);
    }
}
