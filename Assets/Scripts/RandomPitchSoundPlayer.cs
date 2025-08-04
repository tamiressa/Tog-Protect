using UnityEngine;

public class RandomPitchSoundPlayer : MonoBehaviour
{
    // The AudioSource component on this GameObject
    private AudioSource audioSource;

    // The audio clip to play
    public AudioClip laserSound;

    [Range(0.1f, 3.0f)]
    public float minPitch = 0.8f; // Lower end of the random pitch range
    [Range(0.1f, 3.0f)]
    public float maxPitch = 1.2f; // Upper end of the random pitch range

    void Awake()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLaserSound()
    {
        // Set a random pitch before playing the sound
        audioSource.pitch = Random.Range(minPitch, maxPitch);

        // Play the sound using PlayOneShot to avoid interrupting existing sounds
        audioSource.PlayOneShot(laserSound);
    }
}


