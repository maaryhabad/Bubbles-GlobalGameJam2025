using UnityEngine;

public class MainScene : MonoBehaviour
{
    public GameObject bubblePrefab; // Prefab da bolha

    public GameObject character; // Personagem
    public float sensitivity = 100f; // Sensibilidade para sopro
    public float spawnRadius = 5f; // Raio de spawn dos círculos

    private AudioClip microphoneInput;
    private const int sampleWindow = 128;
    private float[] audioSamples;

    void Start()
    {
        // Inicializa o microfone
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
        // Lógica para o microfone (som real)
        if (microphoneInput != null && Microphone.IsRecording(null))
        {
            float volume = GetMicrophoneVolume();
            if (volume > sensitivity)
            {
                GenerateBubbles(volume);

                character.GetComponent<Animator>().Play("blowing");

            }
        }
        // Lógica para testes no Editor (simulação de sopro)
        else if (!Application.isMobilePlatform)
        {
            SimulateBlow();
        }
    }

    float GetMicrophoneVolume()
    {
        // Captura uma janela de amostras do microfone
        int microphonePosition = Microphone.GetPosition(null);
        if (microphonePosition < sampleWindow) return 0;

        microphoneInput.GetData(audioSamples, microphonePosition - sampleWindow);
        float sum = 0f;
        foreach (var sample in audioSamples)
        {
            sum += sample * sample;
        }
        return Mathf.Sqrt(sum / sampleWindow) * 1000f; // Escala para valores maiores
    }

    void GenerateBubbles(float volume)
    {
        // Cria a variável minBubbleSize e maxBubbleSize
        float minBubbleSize = 0.1f;
        float maxBubbleSize = 0.5f;

        // Cria a variável spawnPosition
        Vector2 spawnPosition = new Vector2(character.transform.position.x, character.transform.position.y);

        // Instancia o prefab da bolha
        GameObject newBubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);

        // Define um tamanho aleatório para a bolha
        float randomSize = Random.Range(minBubbleSize, maxBubbleSize);
        newBubble.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

        // Adiciona uma força e direção aleatória em um cone para a direita e para cima para a bolha
        float randomAngle = Random.Range(15, 80);
        // Transforma o ângulo de graus para radianos
        randomAngle = randomAngle * Mathf.Deg2Rad;

        float randomForce = Random.Range(1f, 5f);
        Vector2 forceDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        newBubble.GetComponent<Rigidbody2D>().AddForce(forceDirection * randomForce * volume);

    }

    void SimulateBlow()
    {
        // Simula sopro no Editor quando o botão do mouse ou espaço é pressionado
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Debug.Log("Simulando sopro no Editor...");
            GenerateBubbles(Random.Range(50, 200)); // Simula um volume aleatório
        }
    }
}