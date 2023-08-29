using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// The component on the tooltip window itself
/// </summary>
public class Tooltip : MonoBehaviour
{
    private RectTransform rectTransform;
    public TextMeshProUGUI header;
    public TextMeshProUGUI content;
    public LayoutElement layoutElement;
    public int wrapAt = 20;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string newHeader, string newContent)
    {
        if (string.IsNullOrEmpty(newHeader))
        {
            header.gameObject.SetActive(false);
        }
        else
        {
            header.gameObject.SetActive(true);
            header.SetText(newHeader);
        }

        content.SetText(newContent);
        layoutElement.enabled = (header.text.Length > wrapAt || content.text.Length > wrapAt);
        SetPosition();
    }

    /// <summary>
    ///  Sets the position to the mouse cursor
    ///  while avoiding going off the edge of the screen
    /// </summary>
    private void SetPosition() { 
        transform.position = Input.mousePosition;
        // magic values 1.1, -0.1 etc are the offsets that work well to position the tooltip
        float xPivot = ((rectTransform.sizeDelta.x + rectTransform.position.x) > Screen.width) ? 1.1f : -0.1f;
        float yPivot = ((-rectTransform.sizeDelta.y + rectTransform.position.y) < 0) ? -0.5f : 1.1f;
        rectTransform.pivot = new Vector2(xPivot, yPivot);
    }

}
