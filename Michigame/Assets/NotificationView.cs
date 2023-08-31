using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NotificationView : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowNotificacion(string text)
    {
        StartCoroutine(ShowNotifactionCO(text));
    }

    private IEnumerator ShowNotifactionCO(string text)
    {
        TMP_Text tmpText = GetComponent<TMP_Text>();
        tmpText.SetText($"{text}");
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
