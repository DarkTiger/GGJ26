using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] Image sliceImage;
    [SerializeField] Image image;
    [SerializeField] SeedData seedData;

    [SerializeField] Sprite baseImage, selectedImage;

    public SeedData SeedData => seedData;



    void Start()
    {
        textMeshProUGUI.text = seedData.Cost.ToString();
        image.sprite = seedData.Icon;

        textMeshProUGUI.gameObject.transform.up = Vector3.up;

        image.transform.up = Vector3.up;
    }
    public void SelectItem()
    {       
        //sliceImage.color = UnityEngine.Color.green;
        sliceImage.sprite = selectedImage;
    }
    public void DeselectItem()
    {
        //sliceImage.color = UnityEngine.Color.white;
        sliceImage.sprite = baseImage;
    }
}
