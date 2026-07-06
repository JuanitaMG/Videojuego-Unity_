using UnityEngine;

public class PlayerMissionInteract : MonoBehaviour
{
    private NPCMision currentNPC;

    private void OnTriggerEnter(Collider other)
    {
        var npc = other.GetComponent<NPCMision>();
        if(npc != null)
        {
            currentNPC = npc;
            npc.ShowInteractionUI(true);  // ← muestra “Press R”
        }
    }

    void OnTriggerExit(Collider other)
    {
        var npc = other.GetComponent<NPCMision>();
        if (npc != null && npc == currentNPC)
        {
            npc.ShowInteractionUI(false);
            currentNPC = null;
        }
    }

    void Update()
    {
        if (currentNPC != null && Input.GetKeyDown(KeyCode.R))
        {
            currentNPC.TryGiveMission();
        }
    }
}
