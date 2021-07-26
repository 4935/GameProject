using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time")]
    [Tooltip("Day length in minutes")]
    [SerializeField]
    private float _targetDayLength = 1f; //length of day in minutes


    [SerializeField]
    private float sunVariation = 1.5f;
    [SerializeField]
    private float sunBaseIntensity = 1f;
    public float targetDayLength
    {
        get
        {
            return _targetDayLength;
        }
    }
    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay;
    public float timeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }
    private void UpdateTimeScale()
    {
        _timeScale = 24 / (_targetDayLength / 60);
    }

    private void UpdateTime()
    {
        _timeOfDay += Time.deltaTime * _timeScale / 86400; //seconds in a day
        if (_timeOfDay > 1)
        {
            _timeOfDay -= 1;
        }
    }

    [SerializeField]
    private float _timeScale = 100f;

    public bool pause = false;

    [Header("Lighting")]
    [SerializeField]
    private Light sun;
    [SerializeField]
    private Transform SunRotation;
    private float intensity;
    [SerializeField]
    private Gradient sunColour;



    private void Update()
    {
        if(!pause)
        {
            UpdateTimeScale();
            UpdateTime();
        }
        adjustSunRotation();
        sunIntensity();
        adjustSunColour();
    }

    private void adjustSunRotation() //Rotates sun depending on time of day
    {
        float sunAngle = timeOfDay * 360f;
        SunRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sunAngle));
    }
    private void sunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensity = Mathf.Clamp01(intensity);

        sun.intensity = intensity * sunVariation + sunBaseIntensity;
    }

    private void adjustSunColour()
    {
        sun.color = sunColour.Evaluate(intensity);
    }
}
