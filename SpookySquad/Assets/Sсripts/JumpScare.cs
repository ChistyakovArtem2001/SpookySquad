using UnityEngine;


//---------------- Скрипт для звукового скриммера ----------------//

public class JumpScareScript : MonoBehaviour
{
    public AudioSource jumpScareAudio;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            PlayJumpScareSound();
        }
    }

    private void PlayJumpScareSound()
    {
        if (jumpScareAudio != null)
        {
            jumpScareAudio.Play();
            hasPlayed = true;
            Destroy(gameObject, jumpScareAudio.clip.length + 1f); 
        }
    }
}
