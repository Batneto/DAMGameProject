using UnityEditorInternal;
using UnityEngine;

public class SCR_GEN_EventSystemPersistente : MonoBehaviour
{
    private static SCR_GEN_EventSystemPersistente instance;
    private void Awake()
    {
        //Si ya existe una instancia del EventSystem (de una escena anterior), destruimos la actual
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); //No se destruye el EventSystem al cambiar la escena para evitar que se duplique

    }
}
