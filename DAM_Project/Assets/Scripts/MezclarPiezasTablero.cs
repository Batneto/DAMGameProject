using UnityEngine;
using System;

public class MezclarPiezasTablero : MonoBehaviour
{
    //Referenciar los elementos el tablero los cuales se van a desordenar, los huecos y las piezas
    public Transform hueco01;
    public Transform hueco02;
    public Transform[] piezas;
    public int numMovimientos = 40; //numero de movimientos disponibles que pueden hacer las piezas al mezclarse antes de presentar el tablero con el que el jugador jugara
    public RotarPiezaFantasma piezaFantasmaB; //referenciar la rotacion inicial de lap ieza del fantasmaB


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MezclarPiezasAleatoriamente(); //Método que mezlca de manera aleatoria las piezas cada vez que el jugador inicia el juego

        piezaFantasmaB.transform.rotation = Quaternion.Euler(0, 0, 90); //teniendo en cuenta que la pieza del fantasmaB su rotacion siempre empieza por 90 grados

    }

    void MezclarPiezasAleatoriamente()
    {
        Transform ultimaPiezaMovida = null; //Se guarda la ultima pieza movida 

        for (int i = 0; i < numMovimientos; i++) //Mezclar hasta 40 veces las piezas,teniendo en cuanta las posicines y las disancias de las piezas y los huecos
        {
            Transform[] piezasDisponibles = new Transform[4]; //Array para guardar las piezas que se puedan mover hacia el hueco
            int cantidadPiezasDisponibles = 0;

            for (int j = 0; j < piezas.Length; j++) //Revisar con el array a que distancia estan las piezas del hueco hasta llegar a la ultima del array
            {
                float distanciaX = Math.Abs(piezas[j].position.x - hueco01.position.x);
                float distanciaY = Math.Abs(piezas[j].position.y - hueco01.position.y);

                bool piezaAlLado = (distanciaX > 1.0f && distanciaX < 1.2f && distanciaY < 0.1f) || (distanciaY > 1.0f && distanciaY < 1.2f && distanciaX < 0.1f);

                if (piezaAlLado && piezas[j] != ultimaPiezaMovida) //Comprobar que la pieza esta al lado del hueco y que no sea la ultima que se ha haya movido
                {
                    piezasDisponibles[cantidadPiezasDisponibles] = piezas[j]; //guardar las piezas en el array e ir sumando cada vez que se cumple la condicion
                    cantidadPiezasDisponibles++;
                }
            }

            if (cantidadPiezasDisponibles > 0) 
            {
                int numAleatorio = UnityEngine.Random.Range(0, cantidadPiezasDisponibles); //comprobar si hay piezas disponibles para elegir un numero al azar        
                Transform piezaSeleccionada = piezasDisponibles[numAleatorio];//y guardar la pieza escogida 

                Vector3 posicionAnterior = piezaSeleccionada.position; 
                piezaSeleccionada.position = hueco01.position; //Permite intercambiar la pieza seleccionada al azar con el hueco 
                hueco01.position = posicionAnterior;

                ultimaPiezaMovida = piezaSeleccionada; //guarda la ultima pieza movida para evitar que se elija otra vez de nuevo
            }
        }

    }
}
