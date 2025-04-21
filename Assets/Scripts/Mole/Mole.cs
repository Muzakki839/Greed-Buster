using DG.Tweening;
using UnityEngine;

public abstract class Mole : MonoBehaviour
{
   [Header("Mole Settings")]
   public abstract int Point { get; }
   public abstract float WaitDuration { get; }
   public abstract bool AllowHit { get; }

   // Spawn Point Settings
   [HideInInspector] public int spawnPointID;

   private void Start() { Appear(); }

   // --------- Hit ---------
   public void Hit() { }
   public virtual void HitEffect() { }

   // --------- Show/Hide ---------
   public void Appear()
   {
      transform.DOLocalMoveY(transform.localPosition.y + 1, 0.2f).SetEase(Ease.InBounce);
      DOVirtual.DelayedCall(WaitDuration, Hide);
   }
   public void Hide()
   {
      float _animDuration = 0.1f;

      transform.DOLocalMoveY(transform.localPosition.y - 0.8f, _animDuration).SetEase(Ease.OutBounce);
      Destroy(gameObject, _animDuration + 0.1f);

      MoleSpawner.Instance.SetSpawnPointOccupied(spawnPointID, false);
   }

}
