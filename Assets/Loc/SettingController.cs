using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    [SerializeField] public Slider volumeSlider;
    public float defaultVolume = 80f; // am luong mac dinh
    [SerializeField] public List<AudioSource> audioSources ;
    [SerializeField] public Transform audioRoot;
    public void Start()
    {
        audioSources = new List<AudioSource>(audioRoot.GetComponentsInChildren<AudioSource>());
        volumeSlider.value = defaultVolume;
        UpdateAudioSource(defaultVolume);

    }
    public void Update()
    {
        defaultVolume = volumeSlider.value;
        UpdateAudioSource(defaultVolume);
    }

    public void UpdateAudioSource(float value)
    {
        foreach (AudioSource source  in audioSources)
        {
            if (source != null)
            {
                source.volume = value;
            }
        }
    }
}
