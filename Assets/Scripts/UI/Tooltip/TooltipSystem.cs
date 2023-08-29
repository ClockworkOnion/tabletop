using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> The singleton showing and
/// hiding the tooltip. To be
/// called by individual
/// TooltipTriggers </summary>
public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem inst;
    public Tooltip tooltip;

    private void Awake()
    {
        if (inst == null) inst = this;
    }

    public static void Show(string header, string content) {
        inst.tooltip.gameObject.SetActive(true);
        inst.tooltip.SetText(header, content);
    }

    public static void Hide() { 
        inst.tooltip.gameObject.SetActive(false);
    }
}


