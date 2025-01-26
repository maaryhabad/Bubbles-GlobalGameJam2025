using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MurderScene : MonoBehaviour
{
    public Camera mainCamera;
    public Camera textCamera;

    public TextMeshProUGUI textComponent;
    
    private AudioSource audioSource;

    public AudioClip bubblePop;

    void Start()
    {
        new WaitForSeconds(3f);
        textCamera.enabled = false;
        mainCamera.enabled = true;
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayFirstAudio());
    }

    IEnumerator PlayFirstAudio()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.Stop();
        StartCoroutine(ChangeAudioClip());

        textComponent.enabled = false;
        mainCamera.enabled = false;
        textCamera.enabled = true;
    }

    IEnumerator ChangeAudioClip()
    {
        audioSource.clip = bubblePop;
        audioSource.Play();
        yield return new WaitForSeconds(1f);
        audioSource.Stop();
        SceneManager.LoadScene("Execution");
    }
    // Update is called once per frame
    void Update()
    {
        

    }
}
