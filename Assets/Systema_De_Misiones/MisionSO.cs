using UnityEngine;

public enum Zone
{
    zonaAgua,
    zonaFuego,
    zonaViento,
    zonaTierra,
    zonaDesconocida
}

[CreateAssetMenu(fileName = "Nueva Mision", menuName = "Mision System/Mision")]
public class MisionSO : ScriptableObject
{
    public string misionId;
    public string misionName;

    [TextArea]
    public string description;

    public Zone zone;
    public int recompensaGemas;

    // --- CAMPOS DE OBJETIVO ---
    public string itemIDObjetivo;
    public int cantidadObjetivo = 1;

    public GameObject puntoDeEntrega;

    // --- NUEVOS CAMPOS ---
    [Header("Diálogos del NPC")]
    [TextArea]
    public string dialogoCuandoAceptaMision;
    [TextArea]
    public string dialogoCuandoEstaActiva;

    [TextArea]
    public string dialogoCuandoCompletada;


}
