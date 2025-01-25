using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TelephoneManager : MonoBehaviour
{
    public AudioSource phoneSound;
    public Slider slider;
    public TextMeshProUGUI callerName;
    public TextMeshProUGUI timer;

    private float callTime;
    private bool isCallActive;

    void Start()
    {
        if (phoneSound == null)
        {
            Debug.LogError("AudioSource component is not assigned.");
        }
        else
        {
            //diminuir a velocidade do som
            phoneSound.pitch = 0.5f;

            //quando em dispositivo mobile, fazer o device vibrar
            if (Application.isMobilePlatform)
            {
                Handheld.Vibrate();
            }

            phoneSound.Play();
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
            if (phoneSound != null)
            {
                phoneSound.Stop();
            }

            if (timer != null)
            {
                timer.enabled = true;
            }

            isCallActive = true;
        }

        if (isCallActive)
        {
            callTime += Time.deltaTime;
            UpdateTimerText();
            slider.gameObject.SetActive(false);
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(callTime / 60F);
        int seconds = Mathf.FloorToInt(callTime % 60F);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}