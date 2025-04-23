using System;
using dev.vivekraman.Enums;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace dev.vivekraman.Conditions
{
  [Serializable]
  [GeneratePropertyBag]
  [Condition("Agent Near Target", story: "Bat is near [Target]", category: "Conditions",
    id: "ee626bfd4907a6fb2e07446c02f35f93")]
  public class AgentNearTargetCondition : Condition
  {
    [SerializeReference] public BlackboardVariable<AgentTarget> Target;

    public override bool IsTrue()
    {
      return GameManager.Instance.IsTargetAtIndex(Target.Value, GameManager.Instance.GetPlayerIndex());
    }
  }
}
