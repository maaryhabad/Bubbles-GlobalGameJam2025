using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TelephoneManager : MonoBehaviour
{
    public AudioSource source;
    public Slider slider;
    public TextMeshProUGUI callerName;
    public TextMeshProUGUI timer;

    private float callTime;
    private bool isCallActive;

    public AudioClip[] dialogs;

    void Start()
    {
        if (source == null)
        {
            Debug.LogError("AudioSource component is not assigned.");
        }
        else
        {
            //diminuir a velocidade do som
            source.pitch = 0.5f;

            //quando em dispositivo mobile, fazer o device vibrar
            if (Application.isMobilePlatform)
            {
                Handheld.Vibrate();
            }

            source.Play();
        }

        if (timer == null)
        {
            Debug.LogError("TextMeshProUGUI component for timer is not assigned.");
        }
        else
        {
            timer.enabled = false;
        }

        callTime = 0f;
        isCallActive = false;
    }

    void Update()
    {
        if (slider.value == 1 && !isCallActive)
        {
            if (source != null)
            {
                source.Stop();
            }

            if (timer != null)
            {
                timer.enabled = true;
            }

            isCallActive = true;

            // tocar um por um dos áudios de diálogo
            StartCoroutine(PlayDialogs());
        }

        if (isCallActive)
        {
            callTime += Time.deltaTime;
            UpdateTimerText();
            slider.gameObject.SetActive(false);
        }
    }

    IEnumerator PlayDialogs()
    {
        for (int i = 0; i < dialogs.Length; i++)
        {
            source.pitch = 1f;
            source.clip = dialogs[i];
            source.Play();
            yield return new WaitForSeconds(dialogs[i].length);
        }

        source.Stop();
        SceneManager.LoadScene("Terminal");
        
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(callTime / 60F);
        int seconds = Mathf.FloorToInt(callTime % 60F);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}