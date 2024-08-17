using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StopwatchItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _minutes;
    [SerializeField] private TMP_Text _seconds;

    public void SetTime(int minutes, int seconds)
    {
        _minutes.text = minutes.ToString("00");
        _seconds.text = seconds.ToString("00");
    }
}
