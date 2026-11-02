using UnityEngine;

public class VoiceProximity : MonoBehaviour
{
    [SerializeField] private AudioSource voiceSource;
    private bool played = false;

    private void OnTriggerEnter(Collider other)
    {
        if (played) return;

        if (other.CompareTag("Player"))
        {
            voiceSource.Play();
            played = true;
        }
    }
}