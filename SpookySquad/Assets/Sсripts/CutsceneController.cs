using System.Collections;
using UnityEngine;


//---------------- Скрипт для катсцены ----------------//

public class CutsceneController : MonoBehaviour
{
    public Camera[] cameras;
    private Animator[] cameraAnimators;
    private int currentCameraIndex = 0;
    private float currentSwitchTime = 0;
    private Canvas uiCanvas;
    private bool cutsceneFinished = false;
    public GameObject playerSAD;

    void Start()
    {
        uiCanvas = FindObjectOfType<Canvas>();

        cameraAnimators = new Animator[cameras.Length];
        for (int i = 0; i < cameras.Length; i++)
        {
            cameraAnimators[i] = cameras[i].GetComponent<Animator>();
            if (cameraAnimators[i] != null)
            {
                cameraAnimators[i].gameObject.SetActive(false);
            }
        }

        if (cameras.Length != cameraAnimators.Length)
        {
            Debug.LogError("cam != animator");
            return;
        }

        uiCanvas.enabled = false;

        StartCoroutine(EnableCanvasAfterCutscene());
    }

    void Update()
    {
        if (cutsceneFinished)
        {
            Debug.Log("Cutscene finished");
            return;
        }

        Debug.Log($"Current camera index: {currentCameraIndex}");

        if (Time.time - currentSwitchTime >= GetCameraSwitchTime())
        {
            cameraAnimators[currentCameraIndex].gameObject.SetActive(false);

            if (currentCameraIndex == cameras.Length - 1)
            {
                cutsceneFinished = true;

                if (playerSAD != null)
                {
                    Destroy(playerSAD);
                    Debug.Log("Destroyed playerSAD");
                }
            }

            currentCameraIndex++;
            if (currentCameraIndex >= cameras.Length)
            {
                Debug.Log("Cutscene ended");
                return;
            }

            cameraAnimators[currentCameraIndex].gameObject.SetActive(true);

            currentSwitchTime = Time.time;

            Debug.Log($"Switched to camera {currentCameraIndex}");
        }
    }

    float GetCameraSwitchTime()
    {
        if (currentCameraIndex < cameraAnimators.Length)
        {
            AnimatorClipInfo[] clipInfo = cameraAnimators[currentCameraIndex].GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length > 0)
            {
                return clipInfo[0].clip.length;
            }
        }
        return 0;
    }

    IEnumerator EnableCanvasAfterCutscene()
    {
        while (!cutsceneFinished)
        {
            yield return null;
        }

        uiCanvas.enabled = true;

        Debug.Log("Enabled UI Canvas");
    }
}
