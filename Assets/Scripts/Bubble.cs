using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Bubble : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dragAfterTime = 1f; // Tempo em segundos antes de aplicar o arrasto
    public float dragAmount = 2f; // Quantidade de arrasto a ser aplicada

    public GameObject bubble; // Prefab da bolha
    public GameObject character; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ApplyDragAfterTime(dragAfterTime));
    }

    void FixedUpdate()
    {
        // Se a bolha sair da tela, destrua-a
        if (transform.position.y > 5.5f)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ApplyDragAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        rb.linearDamping = dragAmount; // Aplica o arrasto para reduzir a velocidade
    }

    // Quando clicar na bolha sendo pelo mouse ou pelo toque, mudar de cena
    void OnMouseDown()
    {
        Debug.Log("Clicou na bolha!");
        // Aumenta o tamanho da bolha aos poucos até ela tomar a tela toda
        StartCoroutine(IncreaseSizeOverTime());
        
        // Tirar o rigidbody para parar de se mover
        Destroy(rb);

        // Movimentar a bolha para o centro da tela
        StartCoroutine(MoveToCenter());

        
    }

    // Coroutine para mover a bolha até o centro da tela
    private IEnumerator MoveToCenter()
    {
        while (transform.position != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, 0.03f);
            yield return null;
        }
    }

    // Coroutine to increase the size of the bubble over time
       private IEnumerator IncreaseSizeOverTime()
    {
        Camera mainCamera = Camera.main;
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;
    
        while (transform.localScale.x < screenWidth || transform.localScale.y < screenHeight)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f); // Ajuste o incremento conforme necessário
            yield return new WaitForSeconds(0.02f); // Ajuste o atraso conforme necessário
        }
    
        Debug.Log("Cobri a tela inteira, troque de cena");

        // Trocar de cena
        SceneManager.LoadScene("OpeningCutscene");
    
    }

    public void GenerateBubbles(float volume)
    {
        // Cria a variável minBubbleSize e maxBubbleSize
        float minBubbleSize = 0.1f;
        float maxBubbleSize = 0.5f;

        // Cria a variável spawnPosition
        Vector2 spawnPosition = new Vector2(character.transform.position.x, character.transform.position.y);

        // Instancia o prefab da bolha
        GameObject newBubble = Instantiate(bubble, spawnPosition, Quaternion.identity);

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
}
