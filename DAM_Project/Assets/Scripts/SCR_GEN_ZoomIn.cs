using UnityEngine;

public class SCR_GEN_ZoomIn : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private GameObject canvasFlechas;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void ZoomIn() 
    {
        gameObject.SetActive(true);
        canvasFlechas.SetActive(false);

    }

    public void ZoomOut() 
    {
        gameObject.SetActive(false);
        canvasFlechas.SetActive(true);
    }


}
