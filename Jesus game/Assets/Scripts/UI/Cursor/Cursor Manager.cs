using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    public Texture2D menuCursor;
    public Texture2D normalCursor;
    public Texture2D targetCursor;

    public Camera cam;
    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        updateCursor("normal");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the mouse is over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Set the cursor to the menu cursor when hovering over UI
            updateCursor("menu");
            return; // Skip further raycasting if over UI
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                updateCursor("target");
            }
            else if (hit.collider.name == "map")
            {
                updateCursor("normal");
            }
            else if (hit.collider.CompareTag("menu"))
            {
                updateCursor("menu");
            }
            else
            {
                updateCursor("normal");
            }
        }
    }

    public void updateCursor(string name)
    {
        int num = 8;
        int menuNum = 4;

        if (name == "menu")
        {
            Cursor.SetCursor(menuCursor, new Vector2(menuNum, menuNum), CursorMode.Auto);
        }
        else if (name == "normal")
        {
            Cursor.SetCursor(normalCursor, new Vector2(num, num), CursorMode.Auto);
        }
        else if (name == "target")
        {
            Cursor.SetCursor(targetCursor, new Vector2(num, num), CursorMode.Auto);
        }
        else
        {
            Debug.LogError(name + " is not a valid cursor");
        }
    }
}
