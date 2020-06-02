using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaCursor : MonoBehaviour
{
    public Texture2D CursorTexture;
    private Vector2 hotspot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(CursorTexture, hotspot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
