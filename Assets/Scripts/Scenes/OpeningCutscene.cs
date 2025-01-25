using UnityEngine;

public class OpeningCutscene : MonoBehaviour
{
    public Camera firstCamera;
    public Camera secondCamera;

    void OnEnable() {
        secondCamera.enabled = true;
        firstCamera.enabled = false;
    }
    // public void StartCutscene()
    // {
    //     mainCamera.enabled = false;
    //     secondCamera.enabled = true;
    //     Debug.Log("Cutscene started.");
    // }
}