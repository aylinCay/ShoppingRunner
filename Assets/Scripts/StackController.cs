namespace  ShoppingRush
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class StackController : MonoBehaviour
    {
        public List<ItemObjects> stackObjects = new List<ItemObjects>();

        public Transform pivot;

        private Vector3 _targetPos;

        private Transform _target;

        public void Start()
        {

        }

        public void AddToStack(ItemObjects itemObj)
        {
            stackObjects.Add(itemObj);
        }

        public void RemoveStack(ItemObjects itemObj)
        {
           stackObjects.Remove(itemObj);
        }

        public Transform GetLastObject()
        {
            if (stackObjects.Count == 0)
            {
                return transform;
            }

            return stackObjects[^1].hint;
        }

        public void LateUpdate()
        {
            for (int i = 0; i < stackObjects.Count; i++)
            {
                _target = i == 0 ? pivot : stackObjects[i - 1].hint;
                _targetPos = _target.position;
                
                //if(i != 0)
                //stackObjects[i].pivot.transform.LookAt(_target);

                stackObjects[i].pivot.position = new Vector3(stackObjects[i].pivot.position.x,
                    stackObjects[i].pivot.position.y, pivot.position.z);
                stackObjects[i].pivot.position = Vector3.Lerp(stackObjects[i].pivot.position, _targetPos, .05f);

                if (GameManager.GetInstance().player.isImpact)
                {
                    stackObjects[i].Impact();
                }
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Throw"))
            {
                for (int i = stackObjects.Count -1; i > -1; i--)
                {
                    stackObjects[i].Throw();
                }
                
            }
            
        }
    }

}