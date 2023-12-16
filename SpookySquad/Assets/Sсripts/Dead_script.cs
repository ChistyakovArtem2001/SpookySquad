using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public float deathThreshold = 0f;
    public GameObject deathUI;
    public Transform playerCamera;
    public Button restartButton;

    public GameObject statusBarArea;
    public GameObject image;
    public GameObject quickSlotPanel;

    void Start()
    {
        deathUI.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        CheckPlayerDeath();
    }

    void CheckPlayerDeath()
    {
        if (PlayerState.Instance.currentFear <= deathThreshold)
        {
            PlayerDeathSequence();
        }
    }

    void PlayerDeathSequence()
    {
        deathUI.SetActive(true);

        if (playerCamera != null)
        {
            playerCamera.rotation = Quaternion.Euler(90f, 0f, 0f);
        }

        // ������ �������� ����������
        if (statusBarArea != null)
        {
            statusBarArea.SetActive(false);
        }

        if (image != null)
        {
            image.SetActive(false);
        }

        if (quickSlotPanel != null)
        {
            quickSlotPanel.SetActive(false);
        }

        // �������� ������
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // �������� ������� �������, ����� ���� ������������
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
