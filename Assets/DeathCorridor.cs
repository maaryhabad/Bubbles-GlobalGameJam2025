using System.Collections;
using UnityEngine;

public class DeathCorridor : MonoBehaviour
{
    public AudioClip [] audioClips;

    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("DeathCorridor Start");
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(PlayFirstAudio());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayFirstAudio()
    {
        Debug.Log("Play First Audio");
        audioSource.clip = audioClips[0]; //dona bolha
        audioSource.Play();

        yield return new WaitForSeconds(audioClips[0].length);
        audioSource.Stop();
        StartCoroutine(PlaySecondAudio());
    }
    IEnumerator PlaySecondAudio()
    {
        Debug.Log("Play Second Audio");
        
        audioSource.clip = audioClips[1]; //dona bolha
        audioSource.Play();

        yield return new WaitForSeconds(audioClips[1].length);
        audioSource.Stop();
    }
}
