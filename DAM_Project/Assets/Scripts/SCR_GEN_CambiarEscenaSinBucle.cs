using UnityEngine;

public class CambiarParedSinBucle : MonoBehaviour
{   
    [SerializeField] private GameObject[] puntosCamaraPared;  // Array de las paredes para que la cámara pueda moverse a cada uno
    [SerializeField] private GameObject flechaIzq;
    [SerializeField] private GameObject flechaDer;

    private int indiceActual = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (puntosCamaraPared.Length > 0)           //Checkea que haya paredes y empieza en la posición 0 del array
        {
            MoverCamaraAPared(0);
            flechaDer.gameObject.SetActive(false);  //Al ser el principio, no hay nada más atrás, por tanto se quita el botón
        }
        else
        {
            Debug.LogError("No hay puntos de cámara asignados en el Inspector/mete los gameObjects en Puntos Camara Pared");
        }
    }

    
    public void ParedIzquierda()  //Solo se activa al hacer click en el boton Izquierda
    {
        flechaIzq.gameObject.SetActive(true);
        flechaDer.gameObject.SetActive(true);
        indiceActual++;
        MoverCamaraAPared(indiceActual);

        if(indiceActual==puntosCamaraPared.Length-1)   //Si está en la ultima pared, desactiva el boton izq
        {
            flechaIzq.gameObject.SetActive(false);
        }
    }

    public void ParedDerecha()   //Solo se activa al hacer click en el boton Derecha
    {
            flechaDer.gameObject.SetActive(true);
            flechaIzq.gameObject.SetActive(true);
            indiceActual--;
            MoverCamaraAPared(indiceActual);
            
            if(indiceActual==0)        //Si está en la primera pared, desactiva el boton der
            {
            flechaDer.gameObject.SetActive(false);
            }
    }

    private void MoverCamaraAPared(int indice)
    {
        // Mover este GameObject (debería ser la cámara) al punto seleccionado
        // Solo se mueve en la X del punto correspondiente dentro de puntosCamaraPared
        transform.position = new Vector3(puntosCamaraPared[indice].gameObject.transform.position.x,0,-10);
    }
}
