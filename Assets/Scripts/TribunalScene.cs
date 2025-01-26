using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class TribunalScene : MonoBehaviour
{
    public AudioClip[] audioClips;
    

    void Start()
    {
        StartCoroutine(PlayAudioClipsSequentially());
    }

    private IEnumerator PlayAudioClipsSequentially()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClips[0]; //bubble
        audioSource.Play();

        float time = (audioClips[0].length)/2;
        Debug.Log(time);

        yield return new WaitForSeconds(time);
        StartCoroutine(PlayAudioClip1());

    }

    private IEnumerator PlayAudioClip1()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClips[1];
        audioSource.Play();
        yield return new WaitForSeconds(audioClips[1].length);
        audioSource.Stop();
        StartCoroutine(PlayAudioClip2());
    }

    IEnumerator PlayAudioClip2()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClips[2];
        audioSource.Play();
        yield return new WaitForSeconds(audioClips[2].length);
        audioSource.Stop();
        SceneManager.LoadScene("DeathCorridor");
    }
}