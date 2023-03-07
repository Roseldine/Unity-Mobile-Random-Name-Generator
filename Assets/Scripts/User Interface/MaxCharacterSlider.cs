
using UnityEngine;

public class MaxCharacterSlider : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI textMesh;
    [SerializeField] UnityEngine.UI.Slider slider;

    private void Awake()
    {
        slider.onValueChanged.AddListener(RefreshMaxCharacters);
        RefreshMaxCharacters(slider.value);
    }

    void RefreshMaxCharacters(float value) => textMesh.text = value.ToString();
}