using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MisionDialogoUI : MonoBehaviour
{
    public static MisionDialogoUI instance;


    public GameObject PanelMisionInteractive;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI rewardText;
    public Button aceptarButton;

    private NPCMision currentNPC;
    private MisionUI misionUI;

    private void Awake()
    {
        instance = this;
        PanelMisionInteractive.SetActive(false);
        misionUI = FindFirstObjectByType<MisionUI>();
    }

    public void Show(NPCMision npc)
    {
        currentNPC = npc;

        titleText.text = npc.mision.misionName;
        descriptionText.text = npc.mision.description;
        rewardText.text = "Recompensa: " + npc.mision.recompensaGemas + " gemas";

        aceptarButton.gameObject.SetActive(true);

        PanelMisionInteractive.SetActive(true);
    }

    public void Hide()
    {
        PanelMisionInteractive.SetActive(false);
        currentNPC = null;
        
    }

    private void Start()
    {
        aceptarButton.onClick.AddListener(() =>
        {
            if (currentNPC != null)
            {
                currentNPC.AcceptMission();
                Hide();
                misionUI.PanelHubShow();
            }
        });
    }

    public void ShowMensajeSimple(string textoPlano)
    {
        titleText.text = "Información";
        descriptionText.text = textoPlano;
        rewardText.text = "";
        aceptarButton.gameObject.SetActive(false); // No puede aceptar misiones

        PanelMisionInteractive.SetActive(true);
    }

    public void ShowOfertaMision(NPCMision npc, string dialogoInicio)
    {
        currentNPC = npc;

        // Título de la misión
        titleText.text = npc.mision.misionName;

        // Texto que aparece cuando el NPC te explica la misión
        descriptionText.text = dialogoInicio;

        // Recompensa
        rewardText.text = "Recompensa: " + npc.mision.recompensaGemas + " gemas";

        // Activar botón aceptar
        aceptarButton.gameObject.SetActive(true);

        PanelMisionInteractive.SetActive(true);
    }


}
