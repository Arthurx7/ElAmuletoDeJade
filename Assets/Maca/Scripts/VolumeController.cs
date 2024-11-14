using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Asigna el slider en el inspector
    [SerializeField] private AudioSource audioSource; // Asigna el AudioSource en el inspector

    private void Start()
    {
        // Configura el valor inicial del slider al volumen actual del AudioSource
        if (audioSource != null && volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    private void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
