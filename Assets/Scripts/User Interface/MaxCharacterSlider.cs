
using UnityEngine;

public class MaxCharacterSlider : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI textMesh;
    [SerializeField] UnityEngine.UI.Slider slider;
    [SerializeField] AudioClip clip;

    private void Awake()
    {
        slider.onValueChanged.AddListener(RefreshMaxCharacters);
        RefreshMaxCharacters(slider.value);
    }

    void RefreshMaxCharacters(float value)
    {
        textMesh.text = value.ToString();
        AudioManager.OnPlayAudio?.Invoke(clip);
    }
}