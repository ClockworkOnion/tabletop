using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// On the NetworkManager, to set up the NetworkPrefabsList, which for some
/// reason doesn't work when setting it through the inspector!?
/// </summary>
public class NetworkInit : MonoBehaviour
{
    [SerializeField] private NetworkPrefabsList _networkPrefabsList;

    void Start()
    {
        //RegisterNetworkPrefabs();
    }

    /// <summary>
    /// What was that for? I think I tried something and it's no longer necessary
    /// Something because the network prefabs list was messed up in the old
    /// editor version
    /// </summary>
    private void RegisterNetworkPrefabs() {
        var prefabs = _networkPrefabsList.PrefabList.Select(x => x.Prefab);
        foreach (var prefab in prefabs)
        {
            NetworkManager.Singleton.AddNetworkPrefab(prefab);
            Debug.Log("Added prefab " + prefab.name);
        }
    }

}
