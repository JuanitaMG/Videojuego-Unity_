using UnityEngine;

public class CameraManagerX : MonoBehaviour
{
    public Transform target;          // Player
    public float height = 15f;        // Qué tan arriba está la cámara
    public float distance = 10f;      // Qué tan atrás está
    public float followSmoothness = 0.15f;

    public float zoomSpeed = 5f;
    public float minZoom = 8f;
    public float maxZoom = 25f;

    public float rotateSpeed = 180f;
    Vector3 velocity = Vector3.zero;

    float currentRotationY = 45f;

    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target == null) return;

        HandleZoom();
        HandleRotation();

        // Posición deseada desde arriba + distancia hacia atrás
        Vector3 desiredPos =
            target.position
            - (Quaternion.Euler(0, currentRotationY, 0) * Vector3.forward * distance)
            + Vector3.up * height;

        // Suavizado ultra fluido
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, followSmoothness);

        // Mirar al jugador siempre
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minZoom, maxZoom);
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X");
            currentRotationY += mouseX * rotateSpeed * Time.deltaTime;
        }
    }
}
