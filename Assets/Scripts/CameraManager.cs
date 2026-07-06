using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject targetPlayer;
    public  Vector3 offset;

    private void Start()
    {
        StartCoroutine(InitializeCamera());
    }

     IEnumerator InitializeCamera()
    {
        // Espera un frame para permitir que el Player se inicialice
        yield return null;

        if (targetPlayer == null)
            targetPlayer = GameObject.FindGameObjectWithTag("Player");

        // Fijar posición y rotación absolutas
        transform.position = new Vector3(0f, 2.3f, -4f);
        transform.rotation = Quaternion.Euler(30f, 0f, 0f);

        // Calcular offset real después de que el jugador esté listo
        offset = transform.position - targetPlayer.transform.position;
    }

    private void LateUpdate()
    {
        if (targetPlayer == null) return;

        // Mantener siempre la misma distancia respecto al jugador
        transform.position = targetPlayer.transform.position + offset;

        // La cámara siempre mira al jugador
        transform.LookAt(targetPlayer.transform.position);
    }
}
