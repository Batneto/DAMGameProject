using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Escena que se carga con 'Nueva partida'")]
    [SerializeField] private string escenaNuevaPartida = "Habitacion1";

    [Header("Paneles")]
    [SerializeField] private GameObject panelPrincipal;
    [SerializeField] private GameObject panelCargar;

    void Start()
    {
        Volver();
    }

    public void NuevaPartida()
    {
        SceneManager.LoadScene(escenaNuevaPartida);
    }

    public void AbrirCargar()
    {
        panelPrincipal.SetActive(false);
        panelCargar.SetActive(true);
    }

    public void Volver()
    {
        panelCargar.SetActive(false);
        panelPrincipal.SetActive(true);
    }

    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
