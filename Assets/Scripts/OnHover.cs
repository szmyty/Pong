///-----------------------------------------------------------------
///   Class:       OnHover
///   Description: This class is responsible for increasing the scale of buttons that are being hovered over and decreasing the scale when they are no longer being hovered over.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleSize = 1.2f;

    /// <summary>OnPointerEnter is a Method in the OnHover Class.
    /// <para>This method scales the image up when the pointer is hovering over it.</para>
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(scaleSize, scaleSize, 1f);
    }

    /// <summary>OnPointerExit is a Method in the OnHover Class.
    /// <para>This method scales the image back down to its original size after the pointer is no longer hovering over it.</para>
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
