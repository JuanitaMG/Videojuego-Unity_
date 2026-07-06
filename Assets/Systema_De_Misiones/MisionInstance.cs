using UnityEngine;

public class MisionInstance
{

    public NPCMision npcQueEntrega;

    public MisionSO misionSO;
    public bool estaActiva;
    public bool estaCompletada;

    // --- NUEVO: PROGRESO ---
    public int progresoActual = 0;

    public MisionInstance(MisionSO so)
    {
        misionSO = so;
        estaActiva = false;
        estaCompletada = false;
        progresoActual = 0;
    }

    public void AddProgress()
    {
        if (!estaActiva || estaCompletada) return;

        progresoActual++;

        Debug.Log($"Progreso de {misionSO.misionName}: {progresoActual}/{misionSO.cantidadObjetivo}");

        if (progresoActual >= misionSO.cantidadObjetivo)
        {
            MisionManagerSingleton.singletonInstance.CompleteMission(misionSO.misionId);
        }
    }
}
