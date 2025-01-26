using UnityEngine;

public class NewspaperDrag : MonoBehaviour
{
    private Vector3 lastMousePosition;

    void Start()
    {
        
    }

    void OnMouseDown()
    {
        lastMousePosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
        Vector3 direction = new Vector3(delta.x, delta.y, 0).normalized;
        transform.Translate(direction * Time.deltaTime * 100);

        lastMousePosition = Input.mousePosition;
        
    }

    void Update()
    {
        
    }
}