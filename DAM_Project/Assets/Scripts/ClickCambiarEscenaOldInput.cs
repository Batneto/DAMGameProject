using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickCambiarEscenaOldInput : MonoBehaviour
{
    public string escenaACambiar;
    private void OnMouseDown(){
        SceneManager.LoadScene(escenaACambiar);
    }
}
