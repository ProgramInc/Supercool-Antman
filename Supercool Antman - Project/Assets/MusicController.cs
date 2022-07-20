using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource audioSource;
    public static MusicController Instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Music Volume", 0.5f);
    }

    
    
}
