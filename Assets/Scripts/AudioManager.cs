using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("------- AUDIO SOURCE -------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("------- AUDIO CLIP -------")]
    public AudioClip background;
    public AudioClip death;

    private static AudioManager instance;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Start playing background music if assigned
            if (background != null)
            {
                musicSource.clip = background;
                musicSource.loop = true;
                musicSource.Play();
            }

            sfxSource.mute = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
       
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            // Ensure the sfxSource is not null and is available
            if (!sfxSource.isPlaying || sfxSource.clip != clip)
            {
                sfxSource.mute = false;
                sfxSource.PlayOneShot(clip);
            }
        }
    }

    public void ChangeBackgroundMusic(AudioClip newClip)
    {
        if (newClip != null)
        {
            musicSource.Stop();
            musicSource.clip = newClip;
            musicSource.Play();
        }
    }
}
