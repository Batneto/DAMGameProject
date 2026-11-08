using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_GEN_ClickPuerta : MonoBehaviour
{
    [SerializeField] private string sigEscena;

    public void clickPuerta ()
    {
        if (string.IsNullOrEmpty(sigEscena))
        {

            Debug.LogError("No se ha asignado la siguiente escena.");
        }

        SceneManager.LoadScene(sigEscena);

    }

}
