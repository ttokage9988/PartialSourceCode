using Aha.Common;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

namespace Aha.Main
{
    /// <summary>
    /// アハの正解 / 不正解を判定するクラス
    /// </summary>
    public class AhaAnswer : MonoBehaviour
    {
        public bool IsAnswered { get; set; } = false;

        private Subject<bool> isCorrectedSubject = new Subject<bool>();
        public IObservable<bool> IsCorrectedObservable => isCorrectedSubject;

        [SerializeField] private Camera mainCamera;

        [SerializeField] private LayerMask ahaLayerMask;
        [SerializeField] private LayerMask wrongLayerMask;
        [SerializeField] private LayerMask blockRaycast;

        public Vector2 ClickedPosition { get; private set; }

        private void Update()
        {
            if(!IsAnswered && Input.GetMouseButtonDown(0))
            {
                CheckClick();
            }
        }

        private void CheckClick()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            var blockHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, blockRaycast);
            if (blockHit.collider != null &&
                        (blockHit.collider.gameObject.layer == LayerMask.NameToLayer(GameConstants.HIT_BLOCK_LAYER)))
            {
                // これ以降の当たり判定をスルー
                ClickedPosition = blockHit.point;
                return;
            }

            var correctHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, ahaLayerMask);
            if (correctHit.collider != null && correctHit.collider.CompareTag(GameConstants.HIT_TAG_CORRECT))
            {
                ClickedPosition = correctHit.point;
                Debug.Log("正解です");
                IsAnswered = true;
                isCorrectedSubject.OnNext(true);
                // これ以降の当たり判定をスルー
                return;
            }

            var wrongHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, wrongLayerMask);
            if (wrongHit.collider != null && wrongHit.collider.CompareTag(GameConstants.HIT_TAG_INCORRECT))
            {
                ClickedPosition = wrongHit.point;
                Debug.Log("不正解です");
                IsAnswered = true;
                isCorrectedSubject.OnNext(false);
                // これ以降の当たり判定をスルー
                return;
            }

        }
    }
}