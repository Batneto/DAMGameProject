using UnityEngine;
using UnityEngine.UI;

public class GestionInventario : MonoBehaviour
{

    public Image[] imagenObjetosInventario = new Image[4]; //Array para poder saber la posicion de los objetos en el array
    public int numObjetosGuardados = 0;
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
        //Recoger y guardar el objeto en el inventario
        objetoGuardado[numObjetosGuardados] = objeto;

        //Obtener el sprite del objeto para luego mostrarlo en el inventario
        imagenObjetosInventario[numObjetosGuardados].sprite = objeto.GetComponent<SpriteRenderer>().sprite;
        imagenObjetosInventario[numObjetosGuardados].enabled = true;
        numObjetosGuardados++;

        Debug.Log("El objeto s eha guardado correctamente en el inventario" + objeto.name);
    }

    //Metodo para seleccionar y dejar de seleccionar un objeto del inventario
    public void seleccionarObjetoInventario(int posicion) //recibimos la posicion del objeto que hemos seleccionado
    {
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


}
