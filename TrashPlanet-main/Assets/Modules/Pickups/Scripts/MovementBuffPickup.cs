using GP1.Gameplay;
using UnityEngine;

namespace Modules.Buffs
{
    public class MovementBuffPickup : BasePickup
    {
        protected override void OnPickup()
        {
            MovementManager.Instance.AddBuff(_buff.GetBuff());
        }

        protected override void PlaySound()
        {
            _audioClip?.Play(MovementManager.Instance.GetLastBuffCount(3.5f));
        }

        [SerializeField]
        private MovementBuffSO _buff;
    }
}