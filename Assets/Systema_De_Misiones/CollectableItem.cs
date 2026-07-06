// CollectableItem.cs
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    // Pon aquí el mismo ID que en la misión (ej: "CUBO_HIELO")
    public string missionID = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("HA LLEGADO EL HEROE");
            // Llamamos al método existente que recibe UN parámetro (itemID)
            MisionManagerSingleton.singletonInstance.AddProgress(missionID);
            Destroy(gameObject);
        }
    }
}
