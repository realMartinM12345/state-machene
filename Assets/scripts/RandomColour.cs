using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class RandomColour : MonoBehaviour
{
    private MaterialPropertyBlock propertyBlock;
    private Renderer renderer;

    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        //PickRandom();

    }

    public void PickRandom()
    {
        Color randomColour = Random.ColorHSV();
        propertyBlock.SetColor("_Color", randomColour);
        renderer.SetPropertyBlock(propertyBlock);

    }

    private float H = 0;
    public void SlideColour()
    {
        H += 1f * Time.deltaTime;
        if (H >= 1)
        {
            H = 0;
        }
        Color newColour = Color.HSVToRGB(H, 1, 1);
        propertyBlock.SetColor("_Color", newColour);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetRedColour()
    {
        Color redColor = new Color(1, 0, 0, 1);
        propertyBlock.SetColor("_Color", redColor);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetOrangeColour()
    {
        Color orangeColor = new Color(1, 0.5f, 0, 1);
        propertyBlock.SetColor("_Color", orangeColor);  
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetGreenColour()
    {
        Color greenColor = new Color(0, 0, 1, 1);
        propertyBlock.SetColor("_Color", greenColor);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetBlueColour()
    {
        Color blueColor = new Color(0, 1, 0, 1);
        propertyBlock.SetColor("_Color", blueColor);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetYellowColour()
    {
        Color yellowColor = new Color(0, 1, 0.5f, 1);
        propertyBlock.SetColor("_Color", yellowColor);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetColour(Color newColor)
    {
        propertyBlock.SetColor("_Color", newColor);
        renderer.SetPropertyBlock(propertyBlock);
    }


    
}
