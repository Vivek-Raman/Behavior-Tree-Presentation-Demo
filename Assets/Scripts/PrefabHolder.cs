using UnityEngine;

namespace dev.vivekraman
{
  [CreateAssetMenu]
  public class PrefabHolder : ScriptableObject
  {
    public GameObject agentPrefab;
    public GameObject enemyPrefab;
    public GameObject foodPrefab;
  }
}
