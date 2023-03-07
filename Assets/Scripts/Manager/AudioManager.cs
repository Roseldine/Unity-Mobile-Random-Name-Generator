
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource uiSource;
    [SerializeField] bool canPlayAudio = true;
    [SerializeField] float minPitch = 1;
    [SerializeField] float maxPitch = 1.25f;

    public static readonly EventHandler<AudioClip>  OnPlayAudio = new EventHandler<AudioClip>();
    public static readonly EventHandler<bool>       OnToggleAudio = new EventHandler<bool>();


    private void Awake()
    {
        OnPlayAudio.AddListener(PlayAudio);
        OnToggleAudio.AddListener(ToggleAudio);
    }


    void PlayAudio(AudioClip clip)
    {
        if (canPlayAudio == false)
            return;

        uiSource.pitch = Random.Range(minPitch, maxPitch);
        uiSource.PlayOneShot(clip);
    }

    void ToggleAudio(bool value) => canPlayAudio = value;
}
