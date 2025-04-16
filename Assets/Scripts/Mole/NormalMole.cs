using UnityEngine;

public class NormalMole : Mole
{
   public override int Point { get; set; } = 100;
   public override bool AllowHit { get; } = true;

}
