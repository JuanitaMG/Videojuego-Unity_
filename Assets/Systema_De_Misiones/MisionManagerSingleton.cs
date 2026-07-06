using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class MisionManagerSingleton : MonoBehaviour
{

    public MisionCompletadaUI uiMisionCompletada; // arrastraR en el inspector
    public static MisionManagerSingleton singletonInstance { get; private set; }

   private Dictionary<string, MisionInstance> misiones = new Dictionary<string, MisionInstance>();

    
    // Eventos simples
    public delegate void OnMissionChanged();
    public event OnMissionChanged onMissionChanged;

    private void Awake()
    {
        if (singletonInstance == null) singletonInstance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        
    }

    // Registrar misiones desde assets al iniciar (opcional)
    public void RegisterMission(MisionSO so)
    {
        if (so == null || string.IsNullOrEmpty(so.misionId)) return;
        if (!misiones.ContainsKey(so.misionId))
            misiones.Add(so.misionId, new MisionInstance(so));
    }

    public bool AcceptMission(string missionId)
    {
        if (!misiones.ContainsKey(missionId)) return false;
        var inst = misiones[missionId];
        if (inst.estaActiva || inst.estaCompletada) return false;
        inst.estaActiva = true;
        onMissionChanged?.Invoke();
        return true;
    }

    public bool CompleteMission(string missionId)
    {
        if (!misiones.ContainsKey(missionId)) return false;
        var inst = misiones[missionId];
        if (!inst.estaActiva || inst.estaCompletada) return false;

        inst.estaCompletada = true;
        inst.estaActiva = false;

        Debug.Log($"MISION COMPLETADA: {inst.misionSO.misionName}");

        uiMisionCompletada.MostrarMensaje(inst.misionSO.misionName);

        // Recompensa
        var player = FindFirstObjectByType<PlayerData>();
        if (player != null) player.AgregarGemas(inst.misionSO.recompensaGemas);

        onMissionChanged?.Invoke();
        return true;
    }

    public List<MisionInstance> GetActiveMissions()
    {
        return misiones.Values.Where(m => m.estaActiva).ToList();
    }

    public MisionInstance GetMissionInstance(string missionId)
    {
        misiones.TryGetValue(missionId, out var inst);
        return inst;
    }
    public void AddProgress(string itemID)
    {
        foreach (var m in misiones.Values)
        {
            if (m.estaActiva &&
                !m.estaCompletada &&
                m.misionSO.itemIDObjetivo == itemID)
            {
                m.AddProgress();
                onMissionChanged?.Invoke();

                // FORZAR ACTUALIZACIÓN DE LA UI
                MisionUI ui = FindFirstObjectByType<MisionUI>();
                if (ui != null)
                {
                    Debug.Log("UI FORZADA A REFRESCAR");
                    ui.Refresh();
                }

                return;
            }
        }
    }

    public void ActivarObjetosDeMision(string missionId)
    {
        // Buscar TODOS los CollectableItem que existan en la escena
        CollectableItem[] todos = Resources.FindObjectsOfTypeAll<CollectableItem>();

        foreach (var obj in todos)
        {
            if (obj.missionID == missionId)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}


