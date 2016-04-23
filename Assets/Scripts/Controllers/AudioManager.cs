using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioClip GameLoop;
    private AudioSource _source;

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        if (_source == null)
        {
            _source = GetComponent<AudioSource>();
        } 
        _source.loop = true;
        _source.clip = GameLoop;
        if (!_source.isPlaying) { _source.Play(); }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
