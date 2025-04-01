using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private float volMin;
    [SerializeField] private float volMax;
    [SerializeField] private float volDefault;
    [SerializeField] private float pitchMin;
    [SerializeField] private float pitchMax;
    [SerializeField] private float pitchDefault;
    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;
    [SerializeField] private float speedDefault;

    [SerializeField] private TMP_Text volAmount;
    [SerializeField] private TMP_Text pitchAmount;
    [SerializeField] private TMP_Text speedAmount;
    
    [SerializeField] private Slider volSlider;
    [SerializeField] private Slider pitchSlider;
    [SerializeField] private Slider speedSlider;
    
    [SerializeField] private Speaker _speaker;

    private void Awake()
    {
        //Set up all the basic data the display needs. Only super important for this menu, not for a game implementation
        
        volAmount.text = (Mathf.Round(volDefault * 10f) / 10f).ToString();
        pitchAmount.text = (Mathf.Round(pitchDefault * 10f) / 10f).ToString();
        speedAmount.text = (Mathf.Round(speedDefault * 10f) / 10f).ToString();

        volSlider.maxValue = volMax;
        volSlider.minValue = volMin;

        pitchSlider.maxValue = pitchMax;
        pitchSlider.minValue = pitchMin;

        speedSlider.maxValue = speedMax;
        speedSlider.minValue = speedMin;
        
        _speaker.volume = volDefault;
        _speaker.pitch = pitchDefault;
        _speaker.speed = speedDefault;

        volSlider.value = volDefault;
        pitchSlider.value = pitchDefault;
        speedSlider.value = speedDefault;
    }

    public void ChangeVolume(float volumeScale)
    {
        float newVolume = volumeScale;

        volAmount.text = (Mathf.Round(newVolume * 10f) / 10f).ToString();
        _speaker.volume = newVolume;
    }

    public void ChangePitch(float pitchScale)
    {
        float newPitch = pitchScale;

        pitchAmount.text = (Mathf.Round(newPitch * 10f) / 10f).ToString();
        _speaker.pitch = newPitch;
    }
    
    
    public void ChangeSpeed(float speedScale)
    {
        float newSpeed = speedScale;

        speedAmount.text = (Mathf.Round(newSpeed * 10f) / 10f).ToString();
        _speaker.speed = newSpeed;
    }
}