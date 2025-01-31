using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExecutionScene : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(PlayAudio());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayAudio() {
        foreach (AudioClip audio in audioClips)
        {
            audioSource.clip = audio;
            audioSource.Play();
            yield return new WaitForSeconds(audio.length);
            audioSource.Stop();
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("All audio clips have been played");
        SceneManager.LoadScene("MainScene");
    }
}
