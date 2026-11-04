using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoverPieza : MonoBehaviour
{
    //Referenciar las variables relacionadas con los huecos del tablero, pieza del fantasma e intrusa y guardar la pieza que 
    //el jugador ha seleccionado 
    public Transform hueco;
    RotarPiezaFantasma piezaFantasma;
    PiezaIntrusaHueco02 piezaIntrusa;
    public Transform segundoHueco;
    static MoverPieza piezaSeleccionada;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Obtener el script del objeto para saber si se trata de la pieza fantasma o la intrusa
    void Start()
    {
        piezaFantasma = GetComponent<RotarPiezaFantasma>();
        piezaIntrusa = GetComponent<PiezaIntrusaHueco02>();
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobar si el jugador ha pulsado el botón izquierdo dentro del mundo de unity y relacionarlo con el script
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Has pulsado el botón izquierdo");

            Vector2 posicionRaton = Mouse.current.position.ReadValue();
            Vector2 posicionUnity = Camera.main.ScreenToWorldPoint(posicionRaton); //convierte la posicion del raton a coordenadas en Unity
            Collider2D piezaClick = Physics2D.OverlapPoint(posicionUnity); //comprueba que objeto con Collider2D ha pulsado el jugador

            //Comprobar si el jugador ha pulsado una pieza y comprobar si ha pulsado el primer hueco
            if (piezaClick != null && piezaSeleccionada == this)
            {
                if (piezaClick.gameObject == hueco.gameObject)
                {
                    //calcular la distancia tanto horizontal como vertical entre la pieza seleccionada y el primer hueco haciendo
                    //la comprobacion correspondiente
                    float distanciaX = Math.Abs(transform.position.x - hueco.position.x);
                    float distanciaY = Math.Abs(transform.position.y - hueco.position.y);

                    if ((distanciaX > 1.0f && distanciaX < 1.2f && distanciaY < 0.1f) || (distanciaY > 1.0f && distanciaY < 1.2f && distanciaX < 0.1f))
                    {
                        Debug.Log("Se ha pulsado Hueco_01 que está al lado de la pieza seleccionada");

                        //Intercambia la posición del hueco con la pieza
                        Vector3 posicionAnterior = transform.position;
                        transform.position = hueco.position;
                        hueco.position = posicionAnterior;
                        piezaSeleccionada = null;
                    }

                }
                //Hacer la misma comprobación pero este caso con el hueco02
                else if (piezaClick.gameObject == segundoHueco.gameObject)
                {
                    float distanciaX2 = Math.Abs(transform.position.x - segundoHueco.position.x);
                    float distanciaY2 = Math.Abs(transform.position.y - segundoHueco.position.y);

                    if ((distanciaX2 > 1.0f && distanciaX2 < 1.2f && distanciaY2 < 0.1f) || (distanciaY2 > 1.0f && distanciaY2 < 1.2f && distanciaX2 < 0.1f))
                    {
                        Debug.Log("Se ha pulsado Hueco_02 que está al lado de la pieza seleccionada");

                        Vector3 posicionAnterior = transform.position;
                        transform.position = segundoHueco.position;
                        segundoHueco.position = posicionAnterior;
                        piezaSeleccionada = null;

                    }

                }
            }

            //Comprobar que el jugador ha seleccionado una pieza normal (que no es ni el fantasma ni la intrusa)
            if (piezaClick != null && piezaClick.gameObject == gameObject && piezaFantasma == null && piezaIntrusa == null)
            {
                Debug.Log("He pulsado la pieza");

                piezaSeleccionada = this;
                Debug.Log("La pieza seleccionada es: " + piezaSeleccionada.name);

                
                //Calcular la distancia de la pieza y el hueco para luego intercambair la posicion, 
                //comprobar que se puede mover hacia el primer o segundo hueco
                float distanciaX = Math.Abs(transform.position.x - hueco.position.x);
                float distanciaY = Math.Abs(transform.position.y - hueco.position.y);

                float distanciaX2 = Math.Abs(transform.position.x - segundoHueco.position.x);
                float distanciaY2 = Math.Abs(transform.position.y - segundoHueco.position.y);

                bool ladoHueco01 = (distanciaX > 1.0f && distanciaX < 1.2f && distanciaY < 0.1f) || (distanciaY > 1.0f && distanciaY < 1.2f && distanciaX < 0.1f);
                bool ladoHueco02 = segundoHueco.gameObject.activeInHierarchy && ((distanciaX2 > 1.0f && distanciaX2 < 1.2f && distanciaY2 < 0.1f) ||
                                                                                (distanciaY2 > 1.0f && distanciaY2 < 1.2f && distanciaX2 < 0.1f));

                //Si la pieza se encuentra entre los dos huecos debe escoger hacia que hueco ir
                if (ladoHueco01 && ladoHueco02)
                {
                    Debug.Log("Elige hacia qué hueco quieres mover la pieza");
                }
                else //si no tiene lo dos huecos y solo tiene uno, comprobar que se puede mover hacia uno de ellos
                {
                    piezaSeleccionada = null;

                    if (distanciaX > 1.0f && distanciaX < 1.2f && distanciaY < 0.1f)
                    {
                        Debug.Log("La pieza se puede mover horizontalmente");

                        Vector3 posicionAnterior = transform.position;
                        transform.position = hueco.position;
                        hueco.position = posicionAnterior;

                    }
                    else if (distanciaY > 1.0f && distanciaY < 1.2f && distanciaX < 0.1f)
                    {
                        Debug.Log("La pieza se puede mover verticalmente");

                        Vector3 posicionAnterior = transform.position;
                        transform.position = hueco.position;
                        hueco.position = posicionAnterior;

                    }
                    else if (segundoHueco.gameObject.activeInHierarchy && distanciaX2 > 1.0f && distanciaX2 < 1.2f && distanciaY2 < 0.1f)
                    {
                        Debug.Log("La pieza se puede mover horizontalmente al segundo hueco");

                        Vector3 posicionAnterior = transform.position;
                        transform.position = segundoHueco.position;
                        segundoHueco.position = posicionAnterior;

                    }
                    else if (segundoHueco.gameObject.activeInHierarchy && distanciaY2 > 1.0f && distanciaY2 < 1.2f && distanciaX2 < 0.1f)
                    {
                        Debug.Log("La pieza se puede mover verticalmente al segundo hueco");

                        Vector3 posicionAnterior = transform.position;
                        transform.position = segundoHueco.position;
                        segundoHueco.position = posicionAnterior;
                    }
                }

            }

        }
    }
}
