using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceRoller : MonoBehaviour
{
    public int sideCount = 6;
    public RollResultEvent resultEvent;
    private PlayerNetworkHandler networkHandler;

    void Start()
    {
        GetComponent<DoubleClickListener>().doubleClicked.AddListener(RollDie);
    }

    void RollDie(PlayerNetworkHandler clickingPlayer)
    {
        int result = Random.Range(1, sideCount + 1);
        clickingPlayer.HandleChatMsgServerRpc("Roll: " + result.ToString() + " by " + clickingPlayer.OwnerClientId.ToString());
        resultEvent.Invoke(clickingPlayer, result);
    }


}

// An easy way to pass value through events. I'll leave this here for now, maybe delete later if it goes unused.
public class RollResultEvent : UnityEvent<PlayerNetworkHandler, int> { }