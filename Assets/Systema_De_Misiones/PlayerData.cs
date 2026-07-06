using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int gemas;
   
    public void AgregarGemas(int cantidad)
    {
        gemas += cantidad;
        Debug.Log("Gemas Actuales: " + gemas);

        //Aca se notifica en la UI
    }
}
