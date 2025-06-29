using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public abstract class Mole : MonoBehaviour
{
   [Header("Mole Settings")]
   public abstract int Point { get; set; }
   public abstract float WaitDuration { get; set; }
   public UnityEvent HitEffectEvent;

   // Spawn Point Settings
   [HideInInspector] public int spawnPointID;

   private void Start() => Appear();

   // --------- Hit ---------
   public void Hit()
   {
      Debug.Log($"Hit mole at hole: {spawnPointID}");
      // HitEffect();
      HitEffectEvent?.Invoke();
      ScoreManager.Instance.StartCoroutine(ScoreManager.Instance.TransferScore(Point));
      // ScoreManager.Instance.AddScore(Point);
      DestroyImmediate(gameObject);
   }
   public virtual void HitEffect()
   {
      
   }

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
   }

   private void OnDestroy()
   {
      MoleSpawner.Instance.SetSpawnPointOccupied(spawnPointID, false);
      MoleSpawner.Instance.SetSpawnPointMole(spawnPointID, null);

      // send inactive slot to Ardity
      SerialMessageHandler.Instance?.SendLedMessage(spawnPointID, false);
   }
}
