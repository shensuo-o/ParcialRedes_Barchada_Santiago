using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerJoins : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private GameObject preferedPrefab;
    [SerializeField] private int players;

    
    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("joined player");

        players = Runner.ActivePlayers.Count();
        if (players <= 2)
        {
            if (players == 1)
            {
                preferedPrefab = player1Prefab;
            }
            else if (players == 2)
            {
                preferedPrefab = player2Prefab;
            }

            if (player == Runner.LocalPlayer)
            {
                Runner.Spawn(preferedPrefab, Vector3.zero, Quaternion.identity);
            }
        }
    }
}
