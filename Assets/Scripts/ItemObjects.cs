

    using System;
    using System.Security.Cryptography;
    using DG.Tweening;
    using HypeFire.Library.Events;
    using HypeFire.Templates.Runner.CharacterController;
    using Unity.VisualScripting;
    using UnityEditor;
    using UnityEngine;
    using Random = UnityEngine.Random;

    namespace ShoppingRush
{
    public class ItemObjects : MonoBehaviour
    {
        public GameObject item;
        public Transform pivot;
        public Transform hint;
        private bool _isCollected = false;

        public float impactForce;

       


        public void Start()
        { 
            
        }

        public void GetStack()
        {
            if (_isCollected) return;

            _isCollected = true;

            GameManager.GetInstance().player.isImpact = false;

            var target = GameManager.GetInstance().player.stackController.GetLastObject();

            pivot.transform.SetParent(target.transform);

            var scale = pivot.transform.localScale;

            pivot.transform.DOLocalJump(pivot.localPosition + (Vector3.up * 1.5f), 1f, 1, .25f).OnStart(() =>
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

        public void Throw()
        {
            pivot.transform.DOLocalJump(pivot.localPosition +( Vector3.up * 5f) , 1f,1,1.25f);
        }

        public void Impact()
        {

            Debug.Log("Impact");
            var randDistance = Random.Range(-5, 5);
            var randDuration = Random.Range(.5f, 1.75f);
            transform.DOLocalMove(new Vector3(randDistance, 0f, transform.position.z + randDistance), randDuration);
            GameManager.GetInstance().player.stackController.RemoveStack(this);
        }

    }
        

    }
    