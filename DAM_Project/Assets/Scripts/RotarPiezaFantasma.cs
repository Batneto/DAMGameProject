using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotarPiezaFantasma : MonoBehaviour
{

    //Se declaran las variables que hacen posible referenciar a los dos huecos del tablero y a la pieza del fantasma
    //Se utilizan variables que permitiran comprobar si las dos piezas del fantasma estan unidas
    //Referenciar que hueco ha seleccionado el jugador para mover la pieza seleccionada
    public Transform hueco;
    public Transform piezaFantasmaA;
    public bool fantasmaCorrecto = false;
    public SpriteRenderer piezaImagenFantasmaA;
    public SpriteRenderer piezaImagenFantasmaB;
    public Transform segundoHueco;
    bool piezaFantasmaAlHueco = false;
    Transform huecoSeleccionado;


    // Update is called once per frame
    void Update()
    {
        //Detecta la poscion del click del raton y comprobar que pieza u objeto a seleccionado el jugador
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 posicionRaton = Mouse.current.position.ReadValue();
            Vector2 posicionUnity = Camera.main.ScreenToWorldPoint(posicionRaton);
            Collider2D piezaClick = Physics2D.OverlapPoint(posicionUnity);

            //Comprobar si el jugador ja seleccionado uno de los dos huecos
            if (piezaFantasmaAlHueco && piezaClick == null)
            {
                piezaFantasmaAlHueco = false;
                huecoSeleccionado = null;
            }

            //que hueco ha seleccionado el jugador para que la pieza seleccionada se mueva hacia ese hueco
            if (piezaFantasmaAlHueco && piezaClick != null)
            {
                if (piezaClick.gameObject == hueco.gameObject)
                {
                    huecoSeleccionado = hueco;

                    Debug.Log("Has elegido el Hueco_01 para mover la pieza del fantasma B");

                }
                else if (piezaClick.gameObject == segundoHueco.gameObject && segundoHueco.gameObject.activeInHierarchy)
                {
                    huecoSeleccionado = segundoHueco;

                    Debug.Log("Has elegido el Hueco_02 para mover la pieza del fantasma B");
                }
                else
                {
                    piezaFantasmaAlHueco = false;
                    huecoSeleccionado = null;
                }

                //Comprobar si la pieza que ha seleccionado el jugador esta al lado de un hueco y si puede desplazarse la pieza hacia ese hueco
                if (huecoSeleccionado != null)
                {
                    float distanciaX = Math.Abs(transform.position.x - huecoSeleccionado.position.x);
                    float distanciaY = Math.Abs(transform.position.y - huecoSeleccionado.position.y);

                    
                    bool huecoAlLado = (distanciaX > 1.0f && distanciaX < 1.2f && distanciaY < 0.1f) || (distanciaY > 1.0f && distanciaY < 1.2f && distanciaX < 0.1f);

                    if (huecoAlLado)
                    {
                        int azar = 1; //Si la pieza del fantasmaB todavia no esta bien unida a la pieza del fantasmaA y el azar es 1 
                        //entonces el azar hara que rote o se deslize

                        if (fantasmaCorrecto == false)
                        {
                            azar = UnityEngine.Random.Range(0, 2);
                        }

                        if (fantasmaCorrecto == false)
                        {
                            Debug.Log("La acción del azar para el hueco que has seleccionado es: " + azar);
                        }

                        if (azar == 0 && fantasmaCorrecto == false) //La pieza del fantasmaB todavia no esta correctamente unida por lo que el azar provoca una rotacion en la pieza
                        {
                            transform.Rotate(0, 0, -90);

                            Debug.Log("El azar provoca que rote la pieza fantasma B");
                        }

                        if (azar == 1 || fantasmaCorrecto == true)//Si el azar es 1 o la pieza del fantasmaB ya esta unida correctamente ya puede deslizar la pieza
                        {
                            Debug.Log("La pieza fantasma B se desliza hacia el hueco seleccionado");

                            Vector3 posicionAnterior = transform.position;
                            transform.position = huecoSeleccionado.position;
                            huecoSeleccionado.position = posicionAnterior;
                        }

                    }

                    //Hacer que el jugador tenga que volver a seleccionar una pieza para poder seguir jugando
                    piezaFantasmaAlHueco = false;
                    huecoSeleccionado = null;
                }
            }
            
            //Detectar cuando el jugador pulsa la pieza fantasmaB, calcular la distancia de la pieza al hueco seleccionado en caso que haya dos huecos al lado
            //para comprobar hacia que hueco puede moverse la pieza
            if (piezaClick != null && piezaClick.gameObject == gameObject)
            {
                Debug.Log("He pulsado la pieza fantasma B");

                float distanciaX = Math.Abs(transform.position.x - hueco.position.x);
                float distanciaY = Math.Abs(transform.position.y - hueco.position.y);

                float distanciaX2 = Math.Abs(transform.position.x - segundoHueco.position.x);
                float distanciaY2 = Math.Abs(transform.position.y - segundoHueco.position.y);

                //Comprobar si la pieza esta junto al hueco uno o al dos y saber a cual puede moverse
                bool ladoHueco01 = (distanciaX > 1.0f && distanciaX < 1.2f && distanciaY < 0.1f) || (distanciaY > 1.0f && distanciaY < 1.2f && distanciaX < 0.1f);

                bool ladoHueco02 = segundoHueco.gameObject.activeInHierarchy && ((distanciaX2 > 1.0f && distanciaX2 < 1.2f && distanciaY2 < 0.1f) ||
                                                                                (distanciaY2 > 1.0f && distanciaY2 < 1.2f && distanciaX2 < 0.1f));

                //En caso que la pieza este entre dos huecos el jugador tendra que elegir hacia que hueco mover la pieza
                if (ladoHueco01 && ladoHueco02)
                {
                    piezaFantasmaAlHueco = true;

                    Debug.Log("La pieza Fantasma B está al lado de Hueco_01 y Hueco_02");
                }

                //Comprobar que la pieza del fantasmaB esta junto al hueco y como solo tiene disponible el 01 el azar decide si rota o se desliza la pieza
                //cuando tenga el hueco01 disponible
                if (ladoHueco01 && ladoHueco02 == false)
                {
                    int azar = 1;

                    if (fantasmaCorrecto == false)
                    {
                        azar = UnityEngine.Random.Range(0, 2);
                    }

                    //Comprobar la misma comprobacion tambien si la pieza esta en posicion horizontal al hueco
                    if (distanciaX > 1.0f && distanciaX < 1.2f && distanciaY < 0.1f)
                    {
                        Debug.Log("La piezaFantasmaB está junto al hueco horizontalmente");

                        if (azar == 0 && fantasmaCorrecto == false)
                        {
                            transform.Rotate(0, 0, -90);

                            Debug.Log("Azar rota la pieza");
                        }

                        if (azar == 1 || fantasmaCorrecto == true)
                        {
                            Debug.Log("La pieza fantasma B se desliza hacia el Hueco_01");

                            Vector3 posicionAnterior = transform.position;
                            transform.position = hueco.position;
                            hueco.position = posicionAnterior;
                        }
                    }

                    //Comprobar la misma comprobacion tambien si la pieza esta en posicion vertical al hueco
                    if (distanciaY > 1.0f && distanciaY < 1.2f && distanciaX < 0.1f)
                    {
                        Debug.Log("La piezaFantasmaB está junto al hueco verticalmente");

                        if (azar == 0 && fantasmaCorrecto == false)
                        {
                            transform.Rotate(0, 0, -90);

                            Debug.Log("Azar rota la pieza");
                        }

                        if (azar == 1 || fantasmaCorrecto == true)
                        {
                            Debug.Log("La pieza fantasma B se desliza hacia el Hueco_01");

                            Vector3 posicionAnterior = transform.position;
                            transform.position = hueco.position;
                            hueco.position = posicionAnterior;
                        }
                    }

                }

                //Cuando el hueco que tiene disponible es el 02 si la pieza del fantasma todavia no es la correcta entonces seguira la comprobacion del azar
                //por lo que rotara o se deslizara al hueco dos cuando si esta disponible, si esta resuelta la pieza del fantasma entonces se podra desplazar hacia el hueco dos
                //de la manera que el jugador quiera
                if (ladoHueco02 && ladoHueco01 == false)
                {
                    Debug.Log("La pieza fantasma B está al lado del Hueco_02");

                    int azar = 1;
                    
                    if (fantasmaCorrecto == false)
                    {
                        azar = UnityEngine.Random.Range(0, 2);
                    }

                    if (azar == 0 && fantasmaCorrecto == false)
                    {
                        transform.Rotate(0, 0, -90);

                        Debug.Log("Azar rota la pieza");
                    }

                    if (azar == 1 || fantasmaCorrecto == true)
                    {
                        Debug.Log("La pieza fantasma B se desliza hacia el Hueco_02");

                        Vector3 posicionAnterior = transform.position;
                        transform.position = segundoHueco.position;
                        segundoHueco.position = posicionAnterior;
                    }

                }

            }
        }

        UnionPiezasFantasma();
    }

    //Metodo que comprueba si las dos piezas del fantasma estan unidas correctamente calculando la distancia tanto horizontal y vertical de ambas piezas
    void UnionPiezasFantasma()
    {

        float distanciaX = transform.position.x - piezaFantasmaA.position.x;
        float distanciaY = Math.Abs(transform.position.y - piezaFantasmaA.position.y);

        if (distanciaX > 1.0f && distanciaX < 1.2f && distanciaY < 0.1f && transform.eulerAngles.z < 0.1f && fantasmaCorrecto == false) //comrpobar que las dos pieza 
        //estan alineadas la una con la otra de manera correcta y con la rotacion y posicion correctas
        {
            fantasmaCorrecto = true;

            piezaImagenFantasmaA.color = Color.white; // las piezas muestran el color original (dibujo original correcto)
            piezaImagenFantasmaB.color = Color.white;

            Debug.Log("Fantasma correcto: " + fantasmaCorrecto);

            Debug.Log("Las dos piezas del fantasma están juntas y en la posición correcta");
        }
    }
}


