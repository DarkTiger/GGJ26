using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenMouseInteraction : MonoBehaviour
{
    InputAction click, point;
    Vector2 mousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        click = InputSystem.actions.FindAction("Click");
        point = InputSystem.actions.FindAction("Point");
        click.performed += Click;
        point.performed += UpdateMousePos;
    }

    void Click(InputAction.CallbackContext ctx)
    {
        if (!ctx.ReadValueAsButton())
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.transform.gameObject;

            if (hitObj.TryGetComponent(out Ingredient ing))
            {
            }
        }
    }

    void UpdateMousePos(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }
}