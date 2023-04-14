using HypeFire.Templates.Runner.CharacterController;
using UnityEngine;

namespace ShoppingRush
{
    public class PlayerController : Player
    {
        public StackController stackController;

        public float distance;
        public override void Start()
        {
            base.Start();
            GameManager.GetInstance().player = this;
        }
        
    }
}