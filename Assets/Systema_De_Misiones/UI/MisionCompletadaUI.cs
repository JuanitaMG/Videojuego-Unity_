using UnityEngine;
using TMPro;

public class MisionCompletadaUI : MonoBehaviour
{
    public GameObject panel;              // Asigna PanelMisionCompletada
    public TextMeshProUGUI texto;         // Asigna el texto dentro del panel

    float timer = 0f;
    bool mostrando = false;

    void Awake()
    {
        panel.SetActive(false);
    }

    public void MostrarMensaje(string nombreMision)
    {
        texto.text = $"Misión completada: {nombreMision}\nInforma al NPC.";
        panel.SetActive(true);

        timer = 3f;      // se mostrará 3 segundos
        mostrando = true;
    }

    void Update()
    {
        if (!mostrando) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            panel.SetActive(false);
            mostrando = false;
        }
    }
}
