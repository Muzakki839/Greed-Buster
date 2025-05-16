using UnityEngine;

public class NormalMole : Mole
{
   [SerializeField] private int point = 10;
   [SerializeField] private float waitDuration = 1.5f;

   public override int Point
   {
      get => point;
      set => point = value;
   }
   public override float WaitDuration
   {
      get => waitDuration;
      set => waitDuration = value;
   }
}
