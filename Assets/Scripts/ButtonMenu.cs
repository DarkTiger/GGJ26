using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite selected, deselected;

    private void Update()
    {
        if (this == EventSystem.current.currentSelectedGameObject)
        {
            SelectedButton();
        }
    }
    public void SelectedButton()
    {
         image.sprite = selected;
    }
    public void DeSelectedButton()
    {
        image.sprite = deselected;
    }
}
