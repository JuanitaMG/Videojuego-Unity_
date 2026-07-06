using System.Reflection;
using UnityEngine;

public class MisionPuntoDeEntrega : MonoBehaviour
{
    public string misionIdTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var inst = MisionManagerSingleton.singletonInstance.GetMissionInstance(misionIdTarget);
        if (inst != null && inst.estaActiva && !inst.estaCompletada)
        {
            MisionManagerSingleton.singletonInstance.CompleteMission(misionIdTarget);
            Debug.Log("Entrega realizada: " + inst.misionSO.misionName);
            // Opcional: destruir el punto si es solo una entrega
            Destroy(gameObject);
        }
    }
}
