using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource hoverSound;

    // Buton �zerine gelindi�inde ses �alacak
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            hoverSound.Play();
        }
    }
}
