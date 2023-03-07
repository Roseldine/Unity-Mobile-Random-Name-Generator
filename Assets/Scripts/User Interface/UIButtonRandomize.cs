
using UnityEngine;
using UnityEngine.UI;

public class UIButtonRandomize : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] AudioClip clip;

    private void Awake() => button.onClick.AddListener(Randomize);

    void Randomize()
    {
        GameManager.OnRandomize?.Invoke();
        AudioManager.OnPlayAudio?.Invoke(clip);
    }
}