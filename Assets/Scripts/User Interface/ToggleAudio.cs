
using UnityEngine;
using StardropTools.UI;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] UIToggleButton toggleButton;
    [SerializeField] AudioClip clip;

    private void Awake() => toggleButton.OnToggleValue.AddListener(ToggleRandomCharacters);

    void ToggleRandomCharacters(bool value)
    {
        AudioManager.OnToggleAudio?.Invoke(value);
        AudioManager.OnPlayAudio?.Invoke(clip);
    }
}
