using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;
    bool isPlaying;
    void Start() => audioSource = GetComponent<AudioSource>();
    public void OnClickPlayMusic()
    {
        isPlaying = !isPlaying;
        if (isPlaying)
            audioSource.Play();
        else
            audioSource.Stop();
    }
}
