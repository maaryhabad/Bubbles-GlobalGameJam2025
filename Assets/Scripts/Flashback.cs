using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flashback : MonoBehaviour
{

    public AudioClip [] audioClips;

    private AudioSource audiosource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audiosource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(PlayFirstAudio());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    IEnumerator PlayFirstAudio()
    {
        foreach (AudioClip audio in audioClips)
        {
            audiosource.clip = audio;
            audiosource.Play();
            yield return new WaitForSeconds(audio.length);
            audiosource.Stop();
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("All audio clips have been played");

        SceneManager.LoadScene("Murder");
    }
}
