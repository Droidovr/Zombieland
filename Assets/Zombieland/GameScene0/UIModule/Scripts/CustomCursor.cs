using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(_cursorTexture, new Vector2(32f, 32f), CursorMode.Auto);
    }
}
