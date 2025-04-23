using System;
using System.Collections.Generic;
using dev.vivekraman.Enums;
using UnityEngine;

namespace dev.vivekraman
{
  public class GameManager : MonoBehaviour
  {
    [SerializeField] private BatAgent agent;
    [SerializeField] private PrefabHolder prefabHolder;
    [SerializeField] private List<Transform> spawnPoints;

    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
      if (Instance == null)
        Instance = this;
      else
        Destroy(gameObject);
    }

    #endregion
    
    private readonly int[] _agentPositions =
    {
      0, 1,
      0, 0,
    };
    
    private readonly int[] _enemyPositions =
    {
      1, 0,
      1, 0,
    };
    
    private readonly int[] _foodPositions =
    {
      0, 1,
      1, 1,
    };

    private void Start()
    {
      FlushGameObjects();
    }

    public void FlushGameObjects()
    {
      foreach (GameObject o in GameObject.FindGameObjectsWithTag("Agent"))
      {
        Destroy(o);
      }
      foreach (GameObject o in GameObject.FindGameObjectsWithTag("Enemy"))
      {
        Destroy(o);
      }
      foreach (GameObject o in GameObject.FindGameObjectsWithTag("Food"))
      {
        Destroy(o);
      }
      
      for (int i = 0; i < spawnPoints.Count; ++i)
      {
        Transform spawnPoint = spawnPoints[i];
        if (_agentPositions[i] == 1)
        {
          GameObject.Instantiate(prefabHolder.agentPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        if (_enemyPositions[i] == 1)
        {
          GameObject.Instantiate(prefabHolder.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        if (_foodPositions[i] == 1)
        {
          GameObject.Instantiate(prefabHolder.foodPrefab, spawnPoint.position, spawnPoint.rotation);
        }
      }
    }

    public void MoveAgentTo(int targetIndex)
    {
      for (int i = 0; i < _agentPositions.Length; ++i)
      {
        _agentPositions[i] = i == targetIndex ? 1 : 0;
      }
      FlushGameObjects();
    }

    public void EatFoodAt(int targetIndex)
    {
      _agentPositions[targetIndex] = 0;
      FlushGameObjects();
    }

    public bool IsTargetAtIndex(AgentTarget target, int index)
    {
      return target switch
      {
        AgentTarget.Orc => _enemyPositions[index] == 1,
        AgentTarget.Pizza => _foodPositions[index] == 1,
        _ => false,
      };
    }

    public int GetPlayerIndex()
    {
      return Array.IndexOf(_agentPositions, 1);
    }
  }
}
