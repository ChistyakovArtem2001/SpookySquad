using UnityEngine;
using UnityEngine.UI;


//---------------- Скрипт для концовок ----------------//

public class GameEnd : MonoBehaviour
{
    public ParticleSystem particleSystemToDisable;
    public string requiredTool = "Tools";
    public AudioSource escapeAudio;
    public Canvas escapeCanvas;
    public Text escapeText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventorySystem.Instance.CheckForItem(requiredTool))
            {
                DisableParticleSystem();
                PlayEscapeAudio();
                ShowEscapeUI();
            }
        }
    }

    private void DisableParticleSystem()
    {
        if (particleSystemToDisable != null)
        {
            particleSystemToDisable.gameObject.SetActive(false);
        }
    }

    private void PlayEscapeAudio()
    {
        if (escapeAudio != null)
        {
            escapeAudio.Play();
        }
    }

    private void ShowEscapeUI()
    {
 
            if (escapeText != null)
            {
                escapeText.gameObject.SetActive(true);
                escapeText.text = "You escaped";
            }

    }
}
