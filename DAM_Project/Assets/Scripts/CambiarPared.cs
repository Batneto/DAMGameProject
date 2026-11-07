using UnityEngine;
using UnityEngine.UI;

public class CambiarPared : MonoBehaviour
{
    [SerializeField] private RectTransform contenedorFondo;
   // [SerializeField] private float anchoPantalla = 1920f;

    private int numPlanos => contenedorFondo.childCount;

    private int indiceActual = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("CambiarPared arrancado, numPlanos = " + numPlanos);

        if (numPlanos > 0)           //Checkea que haya paredes y empieza en la posición 0 del array
        {
            MoverCamaraAPared(0);
        }
        else
        {
            Debug.LogError("No hay puntos de cámara asignados en el Inspector/mete los gameObjects en Puntos Camara Pared");
        }
    }


    public void Derecha()
    {
        if(indiceActual==numPlanos-1){      
            indiceActual = 0;
            MoverCamaraAPared(0);
        } else {
            indiceActual++;
            MoverCamaraAPared(indiceActual);
        }

        Debug.Log("Indice: " + indiceActual);
    }

    public void Izquierda()
    {
        if(indiceActual==0){
            indiceActual = numPlanos-1;
            MoverCamaraAPared(numPlanos-1);
        } else {
            indiceActual--;
            MoverCamaraAPared(indiceActual);
        }

        Debug.Log("Indice: " + indiceActual);
    }

    private void MoverCamaraAPared(int indice)
    {
        Debug.Log("MoverCamaraAPared llamado con indice = " + indice);

        RectTransform fondo = (RectTransform)contenedorFondo.GetChild(indice);

        Vector2 nuevaPos = new Vector2(-fondo.anchoredPosition.x, contenedorFondo.anchoredPosition.y);
        contenedorFondo.anchoredPosition = nuevaPos;
       
    }
}
