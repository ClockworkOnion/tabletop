using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** <summary>Component on elements that should display a tooltip when hovered over</summary>
*/
public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string header = "";
    [Multiline]
    public string content = "";
    private const float TOOLTIP_DELAY_TIME = 0.5f;
    private LTDescr delay; // "Internal representation of a tween"

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(TOOLTIP_DELAY_TIME, () =>
        {
            TooltipSystem.Show(header, content);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }
}
