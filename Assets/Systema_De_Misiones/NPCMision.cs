using Unity.VisualScripting;
using UnityEngine;

public class NPCMision : MonoBehaviour
{
    public MisionSO mision;

    // UI sobre el NPC (Press R)
    public GameObject pressRUI;

    // -----------------------------
    // MOSTRAR / OCULTAR TEXTO "PRESS R"
    // -----------------------------
    public void ShowInteractionUI(bool show)
    {
        if (pressRUI != null)
            pressRUI.SetActive(show);
    }

    // -----------------------------
    // LLAMADO CUANDO EL JUGADOR PRESIONA R
    // -----------------------------
    public void TryGiveMission()
    {
        if (mision == null) return;

        // Registrar misión si no existe
        MisionManagerSingleton.singletonInstance.RegisterMission(mision);

        // Obtener instancia
        var inst = MisionManagerSingleton.singletonInstance.GetMissionInstance(mision.misionId);
        if (inst == null) return;

        // ---------------------------------------------------
        // 1) SI LA MISIÓN YA ESTÁ COMPLETADA
        // ---------------------------------------------------
        if (inst.estaCompletada)
        {
            string msg = string.IsNullOrEmpty(mision.dialogoCuandoCompletada)
                ? "Ya completaste esta misión. Visita las zonas de los demás elementos."
                : mision.dialogoCuandoCompletada;

            MisionDialogoUI.instance.ShowMensajeSimple(msg);
            return;
        }

        // ---------------------------------------------------
        // 2) SI LA MISIÓN YA ESTÁ ACTIVA
        // ---------------------------------------------------
        if (inst.estaActiva)
        {
            string msg = string.IsNullOrEmpty(mision.dialogoCuandoEstaActiva)
                ? $"Misión en progreso:\n{inst.misionSO.misionName}\nProgreso: {inst.progresoActual}/{inst.misionSO.cantidadObjetivo}"
                : mision.dialogoCuandoEstaActiva;

            MisionDialogoUI.instance.ShowMensajeSimple(msg);
            return;
        }

        // ---------------------------------------------------
        // 3) MISIÓN AÚN NO ACTIVADA → OFRECER MISIÓN
        // ---------------------------------------------------
        string dialogoInicio = string.IsNullOrEmpty(mision.description)
            ? "Tengo una misión para ti."
            : mision.description;

        MisionDialogoUI.instance.ShowOfertaMision(this, dialogoInicio);
    }

    // ---------------------------------------------------
    // ACEPTAR MISIÓN (llamado desde el UI)
    // ---------------------------------------------------
    public void AcceptMission()
    {
        bool ok = MisionManagerSingleton.singletonInstance.AcceptMission(mision.misionId);
        MisionManagerSingleton.singletonInstance.ActivarObjetosDeMision(mision.itemIDObjetivo);


        if (ok)
        {
            Debug.Log($"Misión aceptada: {mision.misionName}");
            MisionDialogoUI.instance.ShowMensajeSimple("Misión aceptada.");
            
        }
        else
        {
            Debug.Log("No se pudo aceptar la misión (ya activa o completada)");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        MisionDialogoUI.instance.Hide();
    }
}
