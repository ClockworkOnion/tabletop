using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class DiceRoller : MonoBehaviour
{
    public int sideCount = 6;
    public RollResultEvent resultEvent = new RollResultEvent();
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

        List<FetchDiceSelector> diceListeners = GameObject.Find("FetchlistPanel").GetComponentsInChildren<FetchDiceSelector>().Cast<FetchDiceSelector>().ToList();
        diceListeners.ForEach((die) => die.ListenToRoll(sideCount, rollResult: result));
    }
}

// An easy way to pass value through events. I'll leave this here for now, maybe delete later if it goes unused.
public class RollResultEvent : UnityEvent<PlayerNetworkHandler, int> { }