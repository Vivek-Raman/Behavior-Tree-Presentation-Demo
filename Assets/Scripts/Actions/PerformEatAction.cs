using System;
using dev.vivekraman.Enums;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace dev.vivekraman.Actions
{
  [Serializable]
  [GeneratePropertyBag]
  [NodeDescription("Perform Eat", story: "[Bat] eats [Target]", category: "Action",
    id: "f6009bd26e7010efb98c8fddaad2e43e")]
  public class PerformEatAction : Action
  {
    [SerializeReference] public BlackboardVariable<BatAgent> Bat;
    [SerializeReference] public BlackboardVariable<AgentTarget> Target;

    protected override Status OnStart()
    {
      if (Target.Value != AgentTarget.Pizza)
      {
        throw new ArgumentOutOfRangeException();
      }
      GameManager.Instance.EatFoodAt(GameManager.Instance.GetPlayerIndex());
      return Status.Success;
    }
  }
}
