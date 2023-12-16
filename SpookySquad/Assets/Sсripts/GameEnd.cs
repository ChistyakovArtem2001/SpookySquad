using UnityEngine;

//---------------- Скрипт на концовку с инструментами ----------------//

public class GameEnd : MonoBehaviour
{
    public ParticleSystem particleSystemToDisable;
    public string requiredTool = "Tools";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventorySystem.Instance.CheckForItem(requiredTool))
            {
                DisableParticleSystem();
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
}
