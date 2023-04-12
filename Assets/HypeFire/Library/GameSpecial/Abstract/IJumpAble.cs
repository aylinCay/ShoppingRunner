using UnityEngine;

namespace HypeFire.Library.GameSpecial.Abstract
{
    public interface IJumpAble
    {
        /// <summary>
        /// is Character jump able
        /// </summary>
        public  bool isJumpAble { get; }
        
        /// <summary>
        /// Jumps to character
        /// </summary>
        /// <param name="force">float => jump power</param>
        /// <param name="direction">Vector3 => jumping direction</param>
        public void Jump(float force, Vector3 direction);
        
        /// <summary>
        /// Stops to jump process or others
        /// </summary>
        public void Stop();
    }
}