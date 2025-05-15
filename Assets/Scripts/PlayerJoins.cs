using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoins : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    private GameObject preferedPrefab;
    public int playerCount = 0;

    
    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("joined player");
        if(playerCount == 0)
        {
            preferedPrefab = player1Prefab;
        }
        else
        {
            preferedPrefab = player2Prefab;
        }
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(preferedPrefab, Vector3.zero, Quaternion.identity);
            playerCount++;
        }
    }
}
