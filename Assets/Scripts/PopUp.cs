using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] Image bg, fg;
    public ScriptableObject popUpData;

    public virtual void UpdateBG(Sprite s)
    {
        bg.sprite = s;
    }
    public virtual void UpdateFG(Sprite s)
    {
        fg.sprite = s;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
