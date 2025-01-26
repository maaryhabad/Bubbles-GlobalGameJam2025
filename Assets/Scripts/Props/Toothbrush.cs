using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Toothbrush : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        SceneManager.LoadScene("Tribunal");
    }
}
