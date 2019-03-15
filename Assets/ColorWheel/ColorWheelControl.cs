// Copyright (c) 2015, Felix Kate All rights reserved.
// Usage of this code is governed by a BSD-style license that can be found in the LICENSE file.

using UnityEngine;
using UnityEngine.UI;

public class ColorWheelControl : MonoBehaviour
{
    //Output Color
    public Color Selection;

    private RawImage backgroundColorSelection;
    private RawImage ballColorSelection;
    private RawImage paddleColorSelection;

    //Control values
    private float outer;
    private Vector2 inner;

    private Vector2 rectTransform;

    private bool dragOuter, dragInner;

    //The Components of the wheel
    private Material mat;
    private RectTransform RectTrans, SelectorOut, SelectorIn;

    private float halfSize;

    //Set up the transforms
    void Start()
    {
        backgroundColorSelection = GameObject.Find("BackgroundSelection").GetComponent<RawImage>();
        ballColorSelection = GameObject.Find("BallSelection").GetComponent<RawImage>();
        paddleColorSelection = GameObject.Find("PaddleSelection").GetComponent<RawImage>();

        //Get the rect transform and make x and y the same to avoid streching
        RectTrans = gameObject.GetComponent<RectTransform>();
        RectTrans.sizeDelta = new Vector2(RectTrans.sizeDelta.x, RectTrans.sizeDelta.x);
        rectTransform = new Vector2(RectTrans.position.x, RectTrans.position.y);
        //Find and scale the children
        SelectorOut = transform.Find("Selector_Out").GetComponent<RectTransform>();
        SelectorIn = transform.Find("Selector_In").GetComponent<RectTransform>();

        SelectorOut.sizeDelta = RectTrans.sizeDelta / 20.0f;
        SelectorIn.sizeDelta = RectTrans.sizeDelta / 20.0f;

        //Calculate the half size
        halfSize = RectTrans.sizeDelta.x / 2;

        //Set the material
        mat = GetComponent<Image>().material;

        //Set first selected value to red (0° rotation and upper right corner in the box)
        Selection = Color.red;

        //Update the material of the box to red
        UpdateMaterial();
    }

    //Update the selectors
    void Update()
    {
        //Drag selector of outer circle
        if (dragOuter)
        {
            //Get mouse direction
            Vector2 dir = RectTrans.position - Input.mousePosition;
            dir.Normalize();

            //Calculate the radians
            outer = Mathf.Atan2(-dir.x, -dir.y);

            //And update
            UpdateMaterial();
            UpdateColor();

            //On mouse release also release the drag
            if (Input.GetMouseButtonUp(0)) dragOuter = false;

            //Drag selector of inner box
        }
        else if (dragInner)
        {
            //Get position inside the box
            Vector2 dir = RectTrans.position - Input.mousePosition;
            dir.x = Mathf.Clamp(dir.x, -halfSize / 2, halfSize / 2) + halfSize / 2;
            dir.y = Mathf.Clamp(dir.y, -halfSize / 2, halfSize / 2) + halfSize / 2;

            //Scale the value to 0 - 1;
            inner = dir / halfSize;

            UpdateColor();

            //On mouse release also releaste the drag
            if (Input.GetMouseButtonUp(0)) dragInner = false;
        }

        //Set the selectors positions
        SelectorOut.localPosition = new Vector3(Mathf.Sin(outer) * halfSize * 0.85f, Mathf.Cos(outer) * halfSize * 0.85f, -1);
        SelectorIn.localPosition = new Vector3(halfSize * 0.5f - inner.x * halfSize, halfSize * 0.5f - inner.y * halfSize, -1);
    }

    //Update the material of the inner box to match the hue color
    void UpdateMaterial()
    {
        Color c = Color.white;

        //Calculation of rgb from degree with a modified 3 wave function
        //Check out http://en.wikipedia.org/wiki/File:HSV-RGB-comparison.svg to understand how it should look
        c.r = Mathf.Clamp(2 / Mathf.PI * Mathf.Asin(Mathf.Cos(outer)) * 1.5f + 0.5f, 0, 1);
        c.g = Mathf.Clamp(2 / Mathf.PI * Mathf.Asin(Mathf.Cos(2 * Mathf.PI * (1.0f / 3.0f) - outer)) * 1.5f + 0.5f, 0, 1);
        c.b = Mathf.Clamp(2 / Mathf.PI * Mathf.Asin(Mathf.Cos(2 * Mathf.PI * (2.0f / 3.0f) - outer)) * 1.5f + 0.5f, 0, 1);

        mat.SetColor("_Color", c);
    }

    //Gets called after changes
    void UpdateColor()
    {
        Color c = Color.white;

        //Calculation of color same as above
        c.r = Mathf.Clamp(2 / Mathf.PI * Mathf.Asin(Mathf.Cos(outer)) * 1.5f + 0.5f, 0, 1);
        c.g = Mathf.Clamp(2 / Mathf.PI * Mathf.Asin(Mathf.Cos(2 * Mathf.PI * (1.0f / 3.0f) - outer)) * 1.5f + 0.5f, 0, 1);
        c.b = Mathf.Clamp(2 / Mathf.PI * Mathf.Asin(Mathf.Cos(2 * Mathf.PI * (2.0f / 3.0f) - outer)) * 1.5f + 0.5f, 0, 1);

        //Add the colors of the inner box
        c = Color.Lerp(c, Color.white, inner.x);
        c = Color.Lerp(c, Color.black, inner.y);

        Selection = c;

        UpdateImages(c);
    }

    public void UpdateImages(Color c)
    {
        if (MoreSettings.isBackgroundColor)
        {
            MoreSettings.backgroundColor = c;
            SetSetting("backgroundColor", c);
            backgroundColorSelection.color = c;
        }
        else if (MoreSettings.isBallColor)
        {
            MoreSettings.ballColor = c;
            SetSetting("ballColor", c);
            ballColorSelection.color = c;
        }
        else if (MoreSettings.isPaddleColor)
        {
            MoreSettings.paddleColor = c;
            SetSetting("paddleColor", c);
            paddleColorSelection.color = c;
        }
    }

    public void SetSetting(string element, Color color)
    {
        PlayerPrefs.SetFloat(element + "R", color.r);
        PlayerPrefs.SetFloat(element + "G", color.g);
        PlayerPrefs.SetFloat(element + "B", color.b);
    }

    //Method for setting the picker to a given color
    public void PickColor(Color c)
    {
        //Get hsb color from the rgb values
        float max = Mathf.Max(c.r, c.g, c.b);
        float min = Mathf.Min(c.r, c.g, c.b);

        float hue = 0;
        float sat = (1 - min);

        if (max == min)
        {
            sat = 0;
        }

        hue = Mathf.Atan2(Mathf.Sqrt(3) * (c.g - c.b), 2 * c.r - c.g - c.b);

        //Set the sliders
        outer = hue;
        inner.x = 1 - sat;
        inner.y = 1 - max;

        //And update them once
        UpdateMaterial();
    }

    //Gets called by an event trigger at a click
    public void OnClick()
    {
        //Check if click was in outer circle
        if (Vector2.Distance(rectTransform, Input.mousePosition) <= halfSize &&
           Vector2.Distance(rectTransform, Input.mousePosition) >= halfSize - halfSize / 4)
        {
            dragOuter = true;
            return;
            //Check if click was in inner box
        }
        else if (Mathf.Abs(rectTransform.x - Input.mousePosition.x) <= halfSize / 2 &&
                Mathf.Abs(rectTransform.y - Input.mousePosition.y) <= halfSize / 2)
        {
            dragInner = true;
            return;
        }
    }

}
