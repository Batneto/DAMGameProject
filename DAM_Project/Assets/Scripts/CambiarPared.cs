using UnityEngine;

public class CambiarPared : MonoBehaviour
{   
    [SerializeField] private GameObject[] puntosCamaraPared;  // Array de puntos fijos

    private int indiceActual = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (puntosCamaraPared.Length > 0)
        {
            MoverCamaraAPared(0);
        }
        else
        {
            Debug.LogError("No hay puntos de cámara asignados en el Inspector");
        }
    }


    public void ParedIzquierda()
    {
        
        if(indiceActual==3){
            indiceActual = 0;
            MoverCamaraAPared(0);
        } else {
            indiceActual++;
            MoverCamaraAPared(indiceActual);
        }
    }

    public void ParedDerecha()
    {
        if(indiceActual==0){
            indiceActual = 3;
            MoverCamaraAPared(3);
        } else {
            indiceActual--;
            MoverCamaraAPared(indiceActual);
        }
    }

    private void MoverCamaraAPared(int indice)
    {
        // Mover la cámara (este GameObject) al punto seleccionado
        // Solo mover en X
        transform.position = new Vector3(puntosCamaraPared[indice].gameObject.transform.position.x,0,-10);
    }
}
