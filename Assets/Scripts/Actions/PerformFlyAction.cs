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
  [NodeDescription("Perform Fly", story: "[Bat] flies [direction]", category: "Action",
    id: "55b05c41a632105684bf610b979b0ef6")]
  public class PerformFlyAction : Action
  {
    [SerializeReference] public BlackboardVariable<BatAgent> Bat;
    [SerializeReference] public BlackboardVariable<FlyDirection> Direction;

    protected override Status OnStart()
    {
      int playerIndex = GameManager.Instance.GetPlayerIndex();
      int targetIndex = -1;
      //
      if (Direction.Value == FlyDirection.Clockwise)
      {
        targetIndex = playerIndex switch
        {
          0 => 1,
          1 => 3,
          3 => 2,
          2 => 1,
          _ => throw new ArgumentOutOfRangeException("playerIndex", "Bad value of playerIndex"),
        };
      }
      else
      {
        targetIndex = playerIndex switch
        {
          0 => 2,
          2 => 3,
          3 => 1,
          1 => 0,
          _ => throw new ArgumentOutOfRangeException("playerIndex", "Bad value of playerIndex"),
        };
      }
      
      GameManager.Instance.MoveAgentTo(targetIndex);

      return Status.Success;
    }
  }
}
