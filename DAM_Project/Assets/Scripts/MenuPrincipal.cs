using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Escena que se carga con 'Nueva partida'")]
    [SerializeField] private string escenaNuevaPartida = "Habitacion1";

    [Header("Paneles")]
    [SerializeField] private GameObject panelPrincipal;
    [SerializeField] private GameObject panelJugar;
    [SerializeField] private GameObject panelOpciones;
    [SerializeField] private GameObject panelCargar;
     [SerializeField] private GameObject panelLogros;

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
    public void AbrirLogros()
    {
        panelPrincipal.SetActive(false);
        panelLogros.SetActive(true);

    }
    public void AbrirJugar()
    {
        panelPrincipal.SetActive(false);
        panelJugar.SetActive(true);

    }
    public void AbrirOpciones()
    {
        panelPrincipal.SetActive(false);
        panelOpciones.SetActive(true);

    }

    public void Volver()
    {
        panelPrincipal.SetActive(true);
        panelCargar.SetActive(false);
        panelJugar.SetActive(false);
        panelOpciones.SetActive(false);
        panelLogros.SetActive(false);
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
