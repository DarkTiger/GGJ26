using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] Image bg, fg;
    public ScriptableObject popUpData;

    public void UpdateBG(Sprite s)
    {
        bg.sprite = s;
    }
    public void UpdateFG(Sprite s)
    {
        fg.sprite = s;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
