using UnityEngine;

public class CambiarPared : MonoBehaviour
{   
    [SerializeField] private GameObject[] puntosCamaraPared;  // Array de las paredes para que la cámara pueda moverse a cada uno

    private int indiceActual = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (puntosCamaraPared.Length > 0)           //Checkea que haya paredes y empieza en la posición 0 del array
        {
            MoverCamaraAPared(0);
        }
        else
        {
            Debug.LogError("No hay puntos de cámara asignados en el Inspector/mete los gameObjects en Puntos Camara Pared");
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
