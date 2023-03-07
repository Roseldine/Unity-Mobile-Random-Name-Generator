
using UnityEngine;
using StardropTools.UI;

public class ToggleRandomCharacterLength : MonoBehaviour
{
    [SerializeField] UIToggleButton toggleButton;

    private void Awake() => toggleButton.OnToggleValue.AddListener(ToggleRandomCharacters);

    void ToggleRandomCharacters(bool value) => GameManager.OnToggleRandomCharacters?.Invoke(value);
}
