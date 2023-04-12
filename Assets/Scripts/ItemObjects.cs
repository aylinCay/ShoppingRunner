using System;
using System.Security.Cryptography;
using DG.Tweening;
using HypeFire.Library.Events;
using HypeFire.Templates.Runner.CharacterController;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



    public class ItemObjects: MonoBehaviour
    {
        public float cost;
        public GameObject item;
        public Rigidbody rigidbodyHint;
        public Transform pivot;
        public Transform hint;
        private bool _isCollected = false;
        public SpringJoint joint;

        public void Start()
        {
            
        }

        public void GetStack()
        {
            if(_isCollected) return;
            
            _isCollected = true;
            
            var target  = GameManager.GetInstance().player.stackController.GetLastObject();
            
            pivot.transform.SetParent(target.transform);
            
            var scale = pivot.transform.localScale;

            pivot.transform.DOLocalJump(pivot.localPosition + (Vector3.up * 1.5f), 1f,1,.25f).OnStart(() =>
            {
                pivot.transform.DOScale(scale * 1.25f, .25f);
                
            }).OnComplete(() =>
            {
                pivot.transform.DOScale(scale, 0.25f);
                pivot.transform.DOLocalMove(Vector3.up, .25f).OnComplete(() =>
                {
                    pivot.transform.SetParent(null);
                    GameManager.GetInstance().player.stackController.AddToStack(this);
                   
                });
            });
        }

       
    }
