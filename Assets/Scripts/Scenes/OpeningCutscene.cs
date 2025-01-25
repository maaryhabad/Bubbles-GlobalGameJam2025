using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningCutscene : MonoBehaviour
{
    public GameObject background;

    public void Start() {
        Debug.Log("Iniciou o opening cutscene");
        // Mudar o background para a imagem "QuartoDetetive"
        background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("QuartoDetetive");

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Telephone");
        }
    }
}
