using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    /* Remember to match SO_Stat script maxHealth */
    [SerializeField] private Color fullHpColor, threeFourthHpColor, oneHalfHpColor, oneFourthHpColor;
    [SerializeField] private Image fill;
    [SerializeField] private TMP_Text txt;
    private Slider slider;
    private float maxValue;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        UpdateText();
        ColorChange();
    }

    void UpdateText()
    {
        txt.text = slider.value + " / " + maxValue;
    }
    void ColorChange()
    {
        if(IsInRange(maxValue * 3 / 4, maxValue) && fill.color != fullHpColor)                                  // 4/4 hp, green
            fill.color = fullHpColor;
        else if (IsInRange(maxValue * 1 / 2, maxValue * 3 / 4) && fill.color != threeFourthHpColor)  // 3/4 hp, yellow
            fill.color = threeFourthHpColor;
        else if (IsInRange(maxValue * 1 / 4, maxValue * 1 / 2) && fill.color != oneHalfHpColor)        // 2/4 hp, orange
            fill.color = oneHalfHpColor;
        else if (IsInRange(0, maxValue * 1 / 4) && fill.color != oneFourthHpColor)                              // 1/4 hp, red
            fill.color = oneFourthHpColor;
    }

    bool IsInRange(float min, float max)
    {
        return min < slider.value && slider.value <= max;
    }

    public void PublicAdjustHealth(int value)
    { slider.value += value; }
    public void PublicResetValue()
    {maxValue = slider.maxValue;}
}
