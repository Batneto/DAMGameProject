using UnityEngine;

public class VictoriaPuzzleDeslizante : MonoBehaviour
{
    //Referenciar todas las variables de las piezas, huecos, la variable de si se ha completado el puzzle correctamente y la imagen del puzzle completada 
    public Transform[] piezas;
    Vector3[] posicionCorrecta;
    public GameObject piezaIntrusa;
    public RotarPiezaFantasma piezaFantasmaB;
    bool victoriaPuzzleDeslizante = false;
    public GameObject imagenPuzzleCompleto;


    void Awake() //Guardar las posiciones originales/correctas de las piezas antes de que se mezclen las piezas del tablero
    {
        posicionCorrecta = new Vector3[piezas.Length];

        for (int i = 0; i < piezas.Length; i++)
        {
            posicionCorrecta[i] = piezas[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ComprobarVictoria(); //Comprobar si el jugador ha resuelto el puzle correctamente

    }

    //Metodo que permite comprobar que las piezas estan colocadas en la posicion correcta, que las piezas del fantasma se vuelven originales ya que se han colocado correctamente entre ellas
    //y que la pieza intrusa ha desaparecido
    void ComprobarVictoria()
    {
        //Suponemos que las piezas estan correctamente colocadas, es decir, estan en su posicion original
        bool piezasBienColocadas = true;

        for (int i = 0; i < piezas.Length; i++) //con el bucle se va recorriendo cada una de ellas para ver en al posicion que se encuentran
        {
            if (Vector3.Distance(piezas[i].position, posicionCorrecta[i]) > 0.01f) //si la ldistancia actual de la pieza es mayor de 0.01f respecto su posicion original, 
                                                                                   //entonces no esta bien colocada
            {
                piezasBienColocadas = false; //por lo que el puzzle no esta completado
            }
        }

        //comprobar la condicion de victoria comentada anteriormente pero traducida a comprobaciones:
        if (piezasBienColocadas == true && piezaIntrusa.activeSelf == false && piezaFantasmaB.fantasmaCorrecto == true && victoriaPuzzleDeslizante == false)
        {
            victoriaPuzzleDeslizante = true; //el jugador ha completado el puzzle correctamente

            for (int i = 0; i < piezas.Length; i++) //Una vez el jugador ha completado el puzzle se hace el bucle para ir recorriendo cada una de las piezas del puzzle
            {
                MoverPieza movimiento = piezas[i].GetComponent<MoverPieza>(); //se obtiene el script del movimiento de cada pieza

                if (movimiento != null)
                {
                    movimiento.enabled = false; //desactivar su movimiento para que una vez completado el puzzle el jugador no pueda moverlas mas
                }
            }

            piezaFantasmaB.enabled = false; //desactiva el script de la pieza del fantasma 

            imagenPuzzleCompleto.SetActive(true); //se activa la imagen completa del puzzle que aparecera durante cinco segundos despues completar el puzle 
            Invoke("VolverRecreativa", 5f);

            Debug.Log("Todas las piezas estan colocadas correctamente");
            Debug.Log("¡Puzzle Completado!");

        }
    }

    void VolverRecreativa() //cuando el puzzle se ha completado correctamente y han pasado los 5s el jugador vuelve a la escena de la recreativa (por programar)
    {
        Debug.Log("Volviendo a la Recreativa");
    }
}
