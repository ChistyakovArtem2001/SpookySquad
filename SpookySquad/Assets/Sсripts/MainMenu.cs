using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//---------------- ������ �������� ���� ----------------//

public class MainMenu : MonoBehaviour
{
    private bool isFirstScene = true;

    void Start()
    {
        isFirstScene = SceneManager.GetActiveScene().buildIndex == 0;
        StartCoroutine(SetupCursor());
    }

    private IEnumerator SetupCursor()
    {
        yield return null;

        if (isFirstScene)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
