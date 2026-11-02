
using UnityEngine;
using UnityEngine.Audio;

public class AdaptiveMusicController : MonoBehaviour
{
    [Header("Fuentes de música")]
    [SerializeField] private AudioSource musicSala;
    [SerializeField] private AudioSource musicPasillo;

    [Header("Snapshots del Audio Mixer")]
    [SerializeField] private AudioMixerSnapshot snapshotSala;
    [SerializeField] private AudioMixerSnapshot snapshotPasillo;

    [SerializeField] private float transitionTime = 1.5f;

    private bool enPasillo = false;

    private void Start()
    {
        musicSala.Play();
        musicPasillo.Stop();
        snapshotSala.TransitionTo(0f);
    }

    public void EntrarAlPasillo()
    {
        if (enPasillo) return;
        enPasillo = true;

        musicSala.Stop();
        musicPasillo.Play();

        snapshotPasillo.TransitionTo(transitionTime);
    }

    public void VolverASala()
    {
        if (!enPasillo) return;
        enPasillo = false;

        musicPasillo.Stop();
        musicSala.Play();

        snapshotSala.TransitionTo(transitionTime);
    }
}
