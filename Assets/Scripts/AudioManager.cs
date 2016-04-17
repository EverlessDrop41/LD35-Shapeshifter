using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip GameLoop;
    private AudioSource _source;

    void Start()
    {
        _source = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
            _source.loop = true;
            _source.clip = GameLoop;
            if (!_source.isPlaying) { _source.Play();}
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            _source.Stop();
            Destroy(this.gameObject);
            return;
        }
    }

    void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
