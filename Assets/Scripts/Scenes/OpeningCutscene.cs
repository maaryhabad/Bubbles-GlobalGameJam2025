using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpeningCutscene : MonoBehaviour
{
    public GameObject firstCamera;
    public Camera secondCamera;

    private AudioSource secondCameraAudioSource;

    void OnEnable() {

        secondCameraAudioSource = secondCamera.GetComponent<AudioSource>();
        secondCamera.enabled = true;
        firstCamera.SetActive(false);
        secondCameraAudioSource.Play();
        // Se o áudio tiver terminado, passar para outra cena
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(secondCameraAudioSource.clip.length);
        SceneManager.LoadScene("Telephone");
    }

}