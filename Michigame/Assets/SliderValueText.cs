using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderValueText : MonoBehaviour
{
   private TMP_Text text;

   private void Awake()
   {
      text = GetComponent<TMP_Text>();
   }

   public void UpdateText(float number)
   {
      text.SetText(number.ToString("00"));
   }
}
