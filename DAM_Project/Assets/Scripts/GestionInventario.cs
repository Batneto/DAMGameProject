using UnityEngine;
using UnityEngine.UI;

public class GestionInventario : MonoBehaviour
{

    public Image[] imagenObjetosInventario = new Image[4]; //Array para poder saber la posicion de los objetos en el array
    public GameObject[] objetoGuardado = new GameObject[4]; //guardar los objetos recogidos en el inventario
    public int objetoSeleccionado = -1; //saber que objeto seleccionamos del inventario (-1: ningun objeto seleccionado)


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Ocultar al principio todos los objetos del inventario, guardando su posicion en el inventario
        for (int i = 0; i < imagenObjetosInventario.Length; i++)
        {
            imagenObjetosInventario[i].enabled = false;
        }
    }

    //Metodo para guardar el objeto al inventario
    public void guardarObjeto(GameObject objeto)
    {
        //Recorrer el array de espacios en el inventario para saber si tenemos un hueco libre
        for (int i = 0; i < objetoGuardado.Length; i++)
        {
            if (objetoGuardado[i] == null)
            {
                objetoGuardado[i] = objeto;
                //mostrar la imagen del objeto guardado en la posicion libre del inventario
                imagenObjetosInventario[i].sprite = objeto.GetComponent<SpriteRenderer>().sprite;
                imagenObjetosInventario[i].enabled = true;

                Debug.Log("El objeto se ha guardado correctamente en el inventario: " + objeto.name);

                break;
            }
        }
    }

    //Metodo para seleccionar y dejar de seleccionar un objeto del inventario
    public void seleccionarObjetoInventario(int posicion) //recibimos la posicion del objeto que hemos seleccionado
    {
        //Comprobar que no hay ningun objeto guardado en esa posicion
        if (objetoGuardado[posicion] == null)
        {
            return;
        }

        if (objetoSeleccionado == posicion)
        {
            imagenObjetosInventario[posicion].transform.localScale = new Vector3(1f, 1f, 1f); //si se pulsa por segunda vez se deselecciona
            objetoSeleccionado = -1;
        }
        else
        {
            //hacer que el objeto vuvla a su tamaño normal
            if (objetoSeleccionado != -1)
            {
                imagenObjetosInventario[objetoSeleccionado].transform.localScale = new Vector3(1f, 1f, 1f);
            }
            objetoSeleccionado = posicion;
            imagenObjetosInventario[posicion].transform.localScale = new Vector3(2f, 2f, 1f); //cambiar los parametros de la escala de la imagen del inventario
        }

    }

    //Metodo para retirar (simulando que se ha utilizado el objeto seleccionado) el objeto del inventario
    public void retirarObjeto()
    {
        if (objetoSeleccionado != -1)
        {
            //comprobar que hay un objeto guardado en la posicion seleccionada
            if (objetoGuardado[objetoSeleccionado] != null)
            {
                Debug.Log("Se ha retirado correctamente del inventario el objeto: " + objetoGuardado[objetoSeleccionado].name);

                objetoGuardado[objetoSeleccionado] = null; //dejar la posicion de ese objeto del array vacia
                imagenObjetosInventario[objetoSeleccionado].enabled = false; //desactivar la imagen del objeto  para que no se vea en el inventario
                imagenObjetosInventario[objetoSeleccionado].transform.localScale = new Vector3(1f, 1f, 1f); //poner el tamaño de la imagen a su original

                objetoSeleccionado = -1; //ya no esta el objeto seleccionado
            }
        }
    }

    //Metodo que indica que objeto se ha seleccionado del inventario
    public GameObject obtenerObjetoSeleccionado()
    {
        if (objetoSeleccionado != -1)
        {
            return objetoGuardado[objetoSeleccionado];
        }

        return null;
    }

}
