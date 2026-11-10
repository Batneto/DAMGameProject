using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_GEN_AbrirMinijuego : MonoBehaviour
{
    [SerializeField] private string minijuego;

    public void clickStart ()
    {
        if (string.IsNullOrEmpty(minijuego))
        {

            Debug.LogError("No se ha asignado la siguiente escena.");
        }

        SceneManager.LoadScene(minijuego);

    }

}
