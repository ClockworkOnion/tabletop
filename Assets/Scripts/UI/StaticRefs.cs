using UnityEngine;

/// <summary>
/// Stores global variables such as UI color values and control
/// related values such as mouse wheel sensitivity.
/// </summary>
public static class  StaticRefs
{
    public static readonly Color activeColor = new Color(.9f, .4f, .4f, 1f) ;
    public static readonly Color defaultColor = new Color(.51f, .58f, .65f, 1f);
    public static readonly Color hoverColor = new Color(.34f, .38f, .45f, 1f);
    public static readonly Color hoverActiveColor = new Color(.91f, .52f, .52f, 1f);

    public static readonly Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    public static float rotationSpeed = 400f; // TODO: Make this adjustable via menu
}

