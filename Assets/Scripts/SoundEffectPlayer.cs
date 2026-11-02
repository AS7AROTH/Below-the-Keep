
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (audioSource == null || audioSource.clip == null)
        {
            Destroy(gameObject);
            return;
        }

        audioSource.Play();

        float duration = audioSource.clip.length /
                         Mathf.Max(Mathf.Abs(audioSource.pitch), 0.01f);

        Destroy(gameObject, duration);
    }
}
