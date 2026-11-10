using UnityEngine;
using UnityEngine.InputSystem; //detectar clicks del raton con Mouse.current

public class ClickObjeto : MonoBehaviour
{

    public GameObject objetoAmpliado;
    public GestionInventario inventario; //referenciar script ClickObjeto.cs para llamar al metodo guardarObjeto() de GestionInventario.cs

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objetoAmpliado.SetActive(false); //se oculta el objeto ampliado al inicio de la escena
    }

    // Update is called once per frame
    void Update()
    {
        //comprobar el click del boton izquierdo del raton y detectar que el click que hace el jugador es sobre el objeto
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            //posicion del raton en la pantalla y convertir ese click o posicion a coordenadas en Unity
            Vector2 posicionRaton = Mouse.current.position.ReadValue();
            Vector2 posicionunity = Camera.main.ScreenToWorldPoint(posicionRaton);

            if (GetComponent<Collider2D>().OverlapPoint(posicionunity)) //comprobar que se ha pulsado sobre el collider2D del objeto seleccionado
            {
                Debug.Log("Se ha pulsado el objeto");

                MostrarObjeto(); //llamará al método que muestra en grande el objeto que el jugador ha seleccionado 
            }
        }

    }

    void MostrarObjeto()
    {
        objetoAmpliado.SetActive(true); //mostrar el objeto ampliado
        GetComponent<Collider2D>().enabled = false; //desabilitar el objeto para que el jugador siga clickandoel objeto original mientras esta el objeto ampliado en pantalla grande

        Invoke("OcultarObjeto", 3f); //ocultar de nuevo el objetoampliado despues de tres segundos
    }

    void OcultarObjeto()
    {
        Debug.Log("Han pasado tres segundos");

        objetoAmpliado.SetActive(false); //despues de los tres segundos se deja de mostrar el objeto por pantalla

        inventario.guardarObjeto(gameObject); //llamar al metodo guardarObjeto() para guardar en el inventario el objeto que se selecciona de la escena

        gameObject.SetActive(false); //el objeto original desaparecera de la pantalla representando que se va a guardar en el inventario
    }
}
