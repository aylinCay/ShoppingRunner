using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerFollower : MonoBehaviour
    {
        public Transform player;

        private Vector3 pos;
        
        private void FixedUpdate()
        {
            pos = transform.position;
            pos.z = player.position.z;
            transform.position = pos;
        }
    }
}