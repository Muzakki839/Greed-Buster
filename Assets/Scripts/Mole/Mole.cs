using UnityEngine;

public abstract class Mole : MonoBehaviour
{
   public abstract int Point { get; set; }
   public abstract bool AllowHit { get; }

   public void Hit() { }
   public virtual void HitEffect() { }

   public void Appear() { }
   public void Hide() { }

}
