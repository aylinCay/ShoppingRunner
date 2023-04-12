using HypeFire.Templates.Runner.CharacterController;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : Player
    {
        public StackController stackController;
        
        public override void Start()
        {
            base.Start();
            GameManager.GetInstance().player = this;
        }
        
    }
}