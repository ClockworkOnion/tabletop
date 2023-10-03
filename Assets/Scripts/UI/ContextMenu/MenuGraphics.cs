using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuGraphics : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI headerTextMeshPro;
    public bool isMouserPointerInside { get; private set; } = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouserPointerInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouserPointerInside = false;
    }

    void Start()
    {
        headerTextMeshPro = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeader(string header) {
        headerTextMeshPro.SetText(header);
    }
}
