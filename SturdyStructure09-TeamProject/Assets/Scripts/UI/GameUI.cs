using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider expSlider;

    private void Start()
    {
        UpdateHPSlider(1);
        UpdateLevel(1);
        UpdateExpSlider(0);
    }

    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    public void UpdateExpSlider(float percentage)
    {
        Debug.Log($"EXP 비율: {percentage}, EXP 값: {PlayerStats.Instance.Exp}, MaxEXP: {PlayerStats.Instance.MaxExp}");
        expSlider.value = percentage;
    }

    public void UpdateLevel(int level)
    {
        levelText.text = $"{level}";
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
