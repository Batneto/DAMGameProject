#if UNITY_EDITOR
using System.IO;
using TMPro;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

// Crea la escena MenuPrincipal con todo montado: Tools > Crear Menú Principal
public static class CrearMenuPrincipal
{
    const string RutaEscena = "Assets/Scenes/MenuPrincipal.unity";

    [MenuItem("Tools/Crear Menú Principal")]
    static void Crear()
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;

        if (File.Exists(RutaEscena) &&
            !EditorUtility.DisplayDialog("Menú principal", "Ya existe MenuPrincipal. ¿Quieres reemplazarla?", "Reemplazar", "Cancelar"))
            return;

        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Cámara
        var camGO = new GameObject("Main Camera") { tag = "MainCamera" };
        var cam = camGO.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.08f, 0.09f, 0.13f);
        cam.orthographic = true;
        camGO.transform.position = new Vector3(0, 0, -10);

        // Canvas
        var canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvasGO.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;

        // EventSystem (Input System nuevo, como en las otras escenas)
        var esGO = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
        esGO.GetComponent<InputSystemUIInputModule>().AssignDefaultActions();

        // Controlador
        var menuGO = new GameObject("MenuController");
        var menu = menuGO.AddComponent<MenuPrincipal>();

        // --- Panel principal ---
        var panelPrincipal = Hijo("PanelPrincipal", canvasGO.transform);
        Estirar(panelPrincipal);

        var titulo = CrearTexto("Titulo", panelPrincipal, "DAM Game Project", 120, FontStyles.Bold);
        titulo.anchorMin = titulo.anchorMax = new Vector2(0.5f, 0.5f);
        titulo.sizeDelta = new Vector2(1400, 180);
        titulo.anchoredPosition = new Vector2(0, 280);

        var botones = Hijo("Botones", panelPrincipal);
        botones.anchorMin = botones.anchorMax = new Vector2(0.5f, 0.5f);
        botones.sizeDelta = new Vector2(600, 450);
        botones.anchoredPosition = new Vector2(0, -100);
        var vl = botones.gameObject.AddComponent<VerticalLayoutGroup>();
        vl.spacing = 25;
        vl.childAlignment = TextAnchor.MiddleCenter;
        vl.childControlWidth = vl.childControlHeight = true;
        vl.childForceExpandWidth = vl.childForceExpandHeight = false;

        var bNueva = CrearBoton("BotonNuevaPartida", botones, "Nueva partida");
        var bCargar = CrearBoton("BotonCargar", botones, "Cargar partida");
        var bSalir = CrearBoton("BotonSalir", botones, "Salir");

        // --- Panel cargar partida ---
        var panelCargar = Hijo("PanelCargar", canvasGO.transform);
        Estirar(panelCargar);
        var fondo = panelCargar.gameObject.AddComponent<Image>();
        fondo.color = new Color(0.05f, 0.06f, 0.09f, 0.95f);

        var tCargar = CrearTexto("Titulo", panelCargar, "Cargar partida", 90, FontStyles.Bold);
        tCargar.anchorMin = tCargar.anchorMax = new Vector2(0.5f, 0.5f);
        tCargar.sizeDelta = new Vector2(1400, 140);
        tCargar.anchoredPosition = new Vector2(0, 280);

        var msg = CrearTexto("Mensaje", panelCargar, "Todavía no hay partidas guardadas.", 48, FontStyles.Normal);
        msg.anchorMin = msg.anchorMax = new Vector2(0.5f, 0.5f);
        msg.sizeDelta = new Vector2(1400, 120);
        msg.anchoredPosition = new Vector2(0, 40);

        var bVolver = CrearBoton("BotonVolver", panelCargar, "Volver");
        bVolver.anchorMin = bVolver.anchorMax = new Vector2(0.5f, 0.5f);
        bVolver.anchoredPosition = new Vector2(0, -200);

        // Referencias del script
        var so = new SerializedObject(menu);
        so.FindProperty("panelPrincipal").objectReferenceValue = panelPrincipal.gameObject;
        so.FindProperty("panelCargar").objectReferenceValue = panelCargar.gameObject;
        so.ApplyModifiedProperties();

        // Botones -> métodos
        UnityEventTools.AddPersistentListener(bNueva.GetComponent<Button>().onClick, menu.NuevaPartida);
        UnityEventTools.AddPersistentListener(bCargar.GetComponent<Button>().onClick, menu.AbrirCargar);
        UnityEventTools.AddPersistentListener(bSalir.GetComponent<Button>().onClick, menu.Salir);
        UnityEventTools.AddPersistentListener(bVolver.GetComponent<Button>().onClick, menu.Volver);

        panelCargar.gameObject.SetActive(false);

        // Guardar escena y ponerla la primera en el Build
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), RutaEscena);
        ConfigurarBuild();

        Debug.Log("MenuPrincipal creado en " + RutaEscena + " y añadido al Build.");
    }

    static void ConfigurarBuild()
    {
        var rutas = new[] { RutaEscena, "Assets/Scenes/Habitacion1.unity", "Assets/Scenes/Habitacion2.unity" };
        var lista = new System.Collections.Generic.List<EditorBuildSettingsScene>();
        foreach (var r in rutas)
            if (File.Exists(r)) lista.Add(new EditorBuildSettingsScene(r, true));
        EditorBuildSettings.scenes = lista.ToArray();
    }

    static RectTransform Hijo(string nombre, Transform padre)
    {
        var go = new GameObject(nombre, typeof(RectTransform));
        go.transform.SetParent(padre, false);
        return go.GetComponent<RectTransform>();
    }

    static void Estirar(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
    }

    static RectTransform CrearTexto(string nombre, Transform padre, string texto, float tamano, FontStyles estilo)
    {
        var rt = Hijo(nombre, padre);
        var t = rt.gameObject.AddComponent<TextMeshProUGUI>();
        t.text = texto;
        t.fontSize = tamano;
        t.fontStyle = estilo;
        t.alignment = TextAlignmentOptions.Center;
        t.color = Color.white;
        t.raycastTarget = false;
        return rt;
    }

    static RectTransform CrearBoton(string nombre, Transform padre, string texto)
    {
        var rt = Hijo(nombre, padre);
        var img = rt.gameObject.AddComponent<Image>();
        img.color = new Color(0.2f, 0.25f, 0.4f);
        var btn = rt.gameObject.AddComponent<Button>();
        btn.targetGraphic = img;
        var colores = btn.colors;
        colores.highlightedColor = new Color(0.8f, 0.85f, 1f);
        colores.pressedColor = new Color(0.6f, 0.65f, 0.8f);
        btn.colors = colores;

        var le = rt.gameObject.AddComponent<LayoutElement>();
        le.preferredWidth = 600;
        le.preferredHeight = 100;
        rt.sizeDelta = new Vector2(600, 100);

        var t = CrearTexto("Texto", rt, texto, 52, FontStyles.Normal);
        Estirar(t);
        return rt;
    }
}
#endif
