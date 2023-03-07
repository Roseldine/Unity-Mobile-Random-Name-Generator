
using UnityEngine;
using UnityEngine.UI;

public class UIButtonGenerate : MonoBehaviour
{
    [SerializeField] Button button;

    private void Awake() => button.onClick.AddListener(Generate);

    void Generate() => GameManager.OnGenerate?.Invoke();
}