using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoins : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("joined player");

        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
