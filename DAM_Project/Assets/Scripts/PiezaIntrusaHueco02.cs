using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PiezaIntrusaHueco02 : MonoBehaviour
{

    //Referenciar si la pieza intrusa se ha eliminado y referenciar el segundo hueco que es el que deja como
    //consecuencia de haber eliminado el jugador dicha pieza
    public Transform hueco02;
    public bool piezaIntrusaEliminada = false;
    public Transform segundoHueco;

    // Update is called once per frame
    void Update()
    {

        //Comprobar la posicion del raton con la posicion de unity
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 posicionRaton = Mouse.current.position.ReadValue();
            Vector2 posicionUnity = Camera.main.ScreenToWorldPoint(posicionRaton);
            Collider2D piezaClick = Physics2D.OverlapPoint(posicionUnity);

            //Comprobar que el jugador ha seleccionado la pieza intrusa
            if (piezaClick != null && piezaClick.gameObject == gameObject)
            {
                Debug.Log("He pulsado la pieza intrusa");

                //Calcular la distancia que hay entre la pieza y el hueco para saber si se puede mover la pieza intrusa hacia el hueco

                float distanciaX = Math.Abs(transform.position.x - hueco02.position.x);
                float distanciaY = Math.Abs(transform.position.y - hueco02.position.y);

                if (distanciaX > 1.0f && distanciaX < 1.2f && distanciaY < 0.1f)
                {
                    Debug.Log("La pieza intrusa está al lado del hueco01 horizontalmente");

                     //la pieza intrusa esta junto al hueco01 horizontalmente por lo que se desactiva la pieza como tal y se activa el segundo hueco
                    segundoHueco.position = transform.position;
                    segundoHueco.gameObject.SetActive(true);
                    piezaIntrusaEliminada = true;
                    gameObject.SetActive(false);
                }

                if (distanciaY > 1.0f && distanciaY < 1.2f && distanciaX < 0.1f)
                {
                    Debug.Log("La pieza intrusa está al lado del hueco01 verticalmente");
                    
                    //la pieza intrusa esta junto al hueco verticalemte por lo que se desactiva la pieza como tal y se activa el segundo hueco
                    segundoHueco.position = transform.position;
                    segundoHueco.gameObject.SetActive(true);
                    piezaIntrusaEliminada = true;
                    gameObject.SetActive(false);

                }

            }
        }

    }
}
