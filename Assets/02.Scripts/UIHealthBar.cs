using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }
    public Image Mask;
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalSize = Mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetValue(float value)
    {
        Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
