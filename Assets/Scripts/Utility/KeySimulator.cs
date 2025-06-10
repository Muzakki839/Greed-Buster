using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.Collections;

public class KeySimulator : Singleton<KeySimulator>
{
   // Test the key simulation by pressing the space key
   private void Update()
   {
      if (Keyboard.current.spaceKey.wasPressedThisFrame)
      {
         StartCoroutine(SimulateKeyPressCoroutine(Key.RightArrow));
      }
   }

   public IEnumerator SimulateKeyPressCoroutine(Key key, float duration = 0.1f)
   {
      var keyboard = InputSystem.GetDevice<Keyboard>();
      if (keyboard != null)
      {
         // Press
         InputSystem.QueueStateEvent(keyboard, new KeyboardState(key));
         InputSystem.Update();
         // Debug.Log($"Simulated key press: {key}");

         yield return new WaitForSeconds(duration);

         // Release
         InputSystem.QueueStateEvent(keyboard, new KeyboardState());
         InputSystem.Update();
         // Debug.Log($"Simulated key release: {key}");
      }
   }
}