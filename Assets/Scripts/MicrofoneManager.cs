using UnityEngine;

public class MicrofoneManager : MonoBehaviour
{
    public GameObject character;
    public float sensitivity = 0.1f;
    private AudioClip microphoneInput;

    private const int sampleWindow = 128;

    private float[] audioSamples;

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            string microphone = Microphone.devices[0];
            microphoneInput = Microphone.Start(microphone, true, 10, 44100);
            audioSamples = new float[sampleWindow];
            Debug.Log($"Usando microfone: {microphone}");
        }
        else
        {
            Debug.LogWarning("Nenhum microfone detectado! Usando som simulado no Editor.");
        }
    }

    void Update()
    {
        if (microphoneInput != null && Microphone.IsRecording(null))
        {
            HandleMicrophoneInput();
        }
        else
        {
            HandleKeyboardInput();
        }
    }

    private void HandleMicrophoneInput()
    {
        float volume = GetMicrophoneVolume();
        if (volume > sensitivity)
        {
            StartBlowing();
        }
        else
        {
            StopBlowing();
        }
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartBlowing();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            StopBlowing();
        }
    }

    private void StartBlowing()
    {
        character.GetComponent<Character>().StartBlowing();
    }

    private void StopBlowing()
    {
        character.GetComponent<Character>().StopBlowing();
    }

    private float GetMicrophoneVolume()
    {
        int microphonePosition = Microphone.GetPosition(null);
        if (microphonePosition < sampleWindow) return 0;

        microphoneInput.GetData(audioSamples, microphonePosition - sampleWindow);
        float sum = 0f;
        foreach (var sample in audioSamples)
        {
            sum += sample * sample;
        }
        return Mathf.Sqrt(sum / sampleWindow) * 1000f;
    }
}