using UnityEngine;

public class DiceSide : MonoBehaviour
{
    public int value = 0;

    private void OnTriggerEnter(Collider other)
    {
        Jukebox.instance.OnDiceCollision();
    }
}
