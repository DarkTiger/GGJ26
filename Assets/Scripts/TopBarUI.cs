using TMPro;
using UnityEngine;

public class TopBarUI : MonoBehaviour
{
    public static TopBarUI Instance;

    [SerializeField] TextMeshPro textMoney, textHappy, textAngry;

    private void Awake()
    {
        if (Instance != null) { Destroy(Instance); } else { Instance = this; }
    }

    private void Update()
    {
        textMoney.text = GameManager.Instance.GetMoney().ToString();
        textHappy.text = GameManager.Instance.GetHappy().ToString();
        textAngry.text = GameManager.Instance.GetAngry().ToString();
    }
}
