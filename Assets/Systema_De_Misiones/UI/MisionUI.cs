using UnityEngine;
using TMPro;
using System.Text;
using System.Collections.Generic;

public class MisionUI : MonoBehaviour
{
    public GameObject MisionHUB;
    public TextMeshProUGUI misionText;
    public TextMeshProUGUI progresoText;
    private void Start()
    {
        Refresh();
    }

    private void OnEnable()
    {
        if (MisionManagerSingleton.singletonInstance != null)
            MisionManagerSingleton.singletonInstance.onMissionChanged += Refresh;
    }

    private void OnDisable()
    {
        if (MisionManagerSingleton.singletonInstance != null)
            MisionManagerSingleton.singletonInstance.onMissionChanged -= Refresh;
    }

    public void Refresh()
    {

        Debug.Log("REFRESH UI LLAMADO");


        // Obtiene misión activa (tu sistema soporta 1 por ahora)
        List<MisionInstance> active = MisionManagerSingleton.singletonInstance.GetActiveMissions();

        if (active == null || active.Count == 0)
        {
            misionText.text = "No hay misiones activas";
            progresoText.text = "";
            return;
        }
        MisionHUB.SetActive(true);
        // Solo mostramos la primera misión
        MisionInstance m = active[0];

        misionText.text = $"{m.misionSO.misionName}\n" +
                          $"Zona: {m.misionSO.zone}\n" +
                          $"Recompensa: {m.misionSO.recompensaGemas}";

        progresoText.text = $"Progreso: {m.progresoActual}/{m.misionSO.cantidadObjetivo}";
        
    }

    public void PanelHubHide()
    {
        MisionHUB.SetActive(false);
    }

    public void PanelHubShow()
    {
        MisionHUB.SetActive(true);
        Refresh();
    }
}
