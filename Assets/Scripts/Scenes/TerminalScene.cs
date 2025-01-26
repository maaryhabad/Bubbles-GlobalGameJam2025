using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerminalScene : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public float typingSpeed = 0.1f;
    private string fullText;
    private string currentText = "";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullText = textComponent.text;
        textComponent.text = "";

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textComponent.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
        SceneManager.LoadScene("CrimeScene");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
