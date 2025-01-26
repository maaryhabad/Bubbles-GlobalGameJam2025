using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpeningCutscene : MonoBehaviour
{
    public GameObject firstCamera;
    public Camera secondCamera;

    void OnEnable() {
        secondCamera.enabled = true;
        firstCamera.SetActive(false);
        secondCamera.GetComponent<AudioSource>().Play();

        // Se o áudio tiver terminado, passar para outra cena
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("Telephone");
    }

}