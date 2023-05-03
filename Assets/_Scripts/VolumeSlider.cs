using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private int index;

    void Start()
    {
        _slider.onValueChanged.AddListener(val => SoundManager.instance.SliderIndexer(index, val));
        _slider.value = 1;
    }
}
