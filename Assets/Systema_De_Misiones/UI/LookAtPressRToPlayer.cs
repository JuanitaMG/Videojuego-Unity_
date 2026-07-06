using UnityEngine;

public class LookAtPressRToPlayer : MonoBehaviour
{
    private PlayerMovementX target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindFirstObjectByType<PlayerMovementX>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.forward);
    }
}
