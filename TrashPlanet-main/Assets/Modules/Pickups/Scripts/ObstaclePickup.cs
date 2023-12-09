using GP1.Gameplay;
using UnityEngine;

namespace Modules.Buffs
{
    public class ObstaclePickup : BasePickup
    {
        protected override void OnPickup()
        {
            for (int i = 0; i < _removeCount; i++)
            {
                MovementManager.Instance.RemoveLastBuff();
            }
            MovementManager.Instance.AddBuff(_debuff.GetBuff());
        }

        [SerializeField]
        private MovementBuffSO _debuff;
        [SerializeField]
        private int _removeCount;
    }
}