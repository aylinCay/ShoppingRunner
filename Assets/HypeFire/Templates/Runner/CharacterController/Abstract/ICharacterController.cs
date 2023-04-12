using HypeFire.Library.Controllers.InputControllers.Abstract;
using HypeFire.Library.Controllers.Move;
using HypeFire.Library.Controllers.Rotate;
using HypeFire.Library.Controllers.Swerve;
using HypeFire.Library.GameSpecial.Abstract;
using UnityEngine;

namespace HypeFire.Templates.Runner.CharacterController.Abstract
{
    public interface ICharacterController : IMoveAble, IJumpAble, IRotateAble
    {
        public  SwerveInputReader swerveReader { get; }
        public  RigidbodyMove rigidbodyMove { get; }
        public  RotateController rotateController { get; }
    }

  
}