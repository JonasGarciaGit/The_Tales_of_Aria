using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomDePassos : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] clips;

    public AudioSource audioSource;
    // Start is called before the first frame update

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
