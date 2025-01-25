using UnityEngine;
using System.Collections;
using TMPro;
using JetBrains.Annotations;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float typingSpeed = 0.1f;
    private string fullText;
    private string currentText = "";

    public GameObject openingCutscene;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fullText = textComponent.text;
        textComponent.text = "";

        audioSource.Play();
        StartCoroutine(TypeText());

        IEnumerator TypeText()
        {
            for (int i = 0; i < fullText.Length; i++)
            {
                currentText += fullText[i];
                textComponent.text = currentText;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // fade out no texto
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartOpeningCutscene()
    {
        openingCutscene.SetActive(true);
    }

    private IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(3f);
            while (textComponent.color.a > 0)
            {
                textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, textComponent.color.a - (Time.deltaTime / 2));
                yield return null;
            }

            // awake no script Opening Cutscene
            StartOpeningCutscene();
        }
}
