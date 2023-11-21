using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthUnit : MonoBehaviour
{
    private Image _image;

    public Image Image => _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
}
