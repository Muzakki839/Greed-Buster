using DG.Tweening;
using UnityEngine;

public abstract class Mole : MonoBehaviour
{
   public abstract int Point { get;}
   public abstract float WaitDuration { get; }
   public abstract bool AllowHit { get; }

   private void Start() { Appear(); }

   public void Hit() { }
   public virtual void HitEffect() { }

   public void Appear()
   {
      transform.DOLocalMoveY(transform.localPosition.y + 1, 0.2f).SetEase(Ease.InBounce);
      DOVirtual.DelayedCall(WaitDuration, Hide);
   }
   public void Hide()
   {
      float _animDuration = 0.2f;

      transform.DOLocalMoveY(transform.localPosition.y - 2, _animDuration).SetEase(Ease.InBounce);
      Destroy(gameObject, _animDuration + 0.1f);
   }

}
