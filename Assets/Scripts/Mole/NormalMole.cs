using UnityEngine;

public class NormalMole : Mole
{
   [SerializeField] private int point = 100;
   [SerializeField] private float waitDuration = 0.2f;
   [SerializeField] private bool allowHit = true;

   public override int Point => point;
   public override float WaitDuration => waitDuration;
   public override bool AllowHit => allowHit;
}
