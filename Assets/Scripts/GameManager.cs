using System;
using System.Collections.Generic;
using dev.vivekraman.Enums;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace dev.vivekraman
{
  public class GameManager : MonoBehaviour
  {
    [SerializeField] private PrefabHolder prefabHolder;
    [SerializeField] private List<Transform> spawnPoints;

    private GameObject _agent;

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
    
    private readonly int[] _agentPosition =
    {
      0, 1,
      0, 0,
    };
    
    private readonly int[] _enemyPositions =
    {
      0, 0,
      2, 0,
    };
    
    private readonly int[] _foodPositions =
    {
      4, 2,
      2, 3,
    };

    private void Start()
    {
      _agent = GameObject.FindWithTag("Agent");
      Assert.IsNotNull(_agent);
      FlushGameObjects();
    }

    public void FlushGameObjects()
    {
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
        if (_agentPosition[i] == 1)
        {
          _agent.transform.position = spawnPoint.position;
        }
        for (int j = 0; j < _enemyPositions[i]; ++j)
        {
          GameObject.Instantiate(prefabHolder.enemyPrefab, GenerateOffset() + spawnPoint.position, spawnPoint.rotation);
        }
        for (int j = 0; j < _foodPositions[i]; ++j)
        {
          GameObject.Instantiate(prefabHolder.foodPrefab, GenerateOffset() + spawnPoint.position, spawnPoint.rotation);
        }
      }
    }

    public void MoveAgentTo(int targetIndex)
    {
      for (int i = 0; i < _agentPosition.Length; ++i)
      {
        _agentPosition[i] = i == targetIndex ? 1 : 0;
      }

      _agent.transform.DOMove(spawnPoints[targetIndex].position, 1f).SetEase(Ease.Linear);
    }

    public void EatFoodAt(int targetIndex)
    {
      --_foodPositions[targetIndex];
      FlushGameObjects();
    }

    public bool IsTargetAtIndex(AgentTarget target, int index)
    {
      return target switch
      {
        AgentTarget.Orc => _enemyPositions[index] > 0,
        AgentTarget.Pizza => _foodPositions[index] > 0,
        _ => false,
      };
    }

    public int GetPlayerIndex()
    {
      return Array.IndexOf(_agentPosition, 1);
    }
    
    private Vector3 GenerateOffset()
    {
      return 1.25f * Random.insideUnitCircle;
    }
  }
}
