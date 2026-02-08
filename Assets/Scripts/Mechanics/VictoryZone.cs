//using Platformer.Gameplay;
//using UnityEngine;
//using static Platformer.Core.Simulation;
//using UnityEngine.UI; // For UI Text
//using TMPro; // at the top



//namespace Platformer.Mechanics
//{
//    /// <summary>
//    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
//    /// </summary>
//    public class VictoryZone : MonoBehaviour
//    {
//        //public Text winText; // assign in Inspector
//        public TMP_Text winText; // replace Text with TMP_Text
//        void OnTriggerEnter2D(Collider2D collider)
//        {
//            var p = collider.gameObject.GetComponent<PlayerController>();
//            if (p != null)
//            {
//                // Schedule the event (your existing system)
//                var ev = Schedule<PlayerEnteredVictoryZone>();
//                ev.victoryZone = this;

//                // Freeze player
//                p.enabled = false;
//                var rb = p.GetComponent<Rigidbody2D>();
//                if (rb != null) rb.linearVelocity = Vector2.zero;

//                // Show the "You Win!" message
//                if (winText != null)
//                    winText.gameObject.SetActive(true);

//                Debug.Log("Player entered victory zone!");
//            }
//        }
//    }
//}


//using Platformer.Gameplay;
//using UnityEngine;
//using static Platformer.Core.Simulation;
//using TMPro;

//namespace Platformer.Mechanics
//{
//    public class VictoryZone : MonoBehaviour
//    {
//        [Header("UI Reference")]
//        public TMP_Text winText; // Drag your TextMeshPro object here in the Inspector

//        void Awake()
//        {
//            // Ensure the text is hidden when the game starts
//            if (winText != null)
//            {
//                winText.gameObject.SetActive(false);
//            }
//        }

//        void OnTriggerEnter2D(Collider2D collider)
//        {
//            // Check if the object entering is the Player
//            var p = collider.gameObject.GetComponent<PlayerController>();

//            if (p != null)
//            {
//                // 1. SHOW THE TEXT
//                if (winText != null)
//                {
//                    // Wake the object up
//                    winText.gameObject.SetActive(true);

//                    // Force the color to be fully visible (Alpha = 1)
//                    Color fullColor = winText.color;
//                    fullColor.a = 1f;
//                    winText.color = fullColor;

//                    // Manually set the message just to be 100% sure
//                    winText.text = "YOU WIN!";
//                }

//                // 2. FREEZE THE PLAYER
//                p.enabled = false;
//                var rb = p.GetComponent<Rigidbody2D>();
//                if (rb != null)
//                {
//                    rb.linearVelocity = Vector2.zero;
//                    rb.bodyType = RigidbodyType2D.Static; // Optional: stops gravity from pulling them down
//                }

//                // 3. TRIGGER GAME EVENT
//                var ev = Schedule<PlayerEnteredVictoryZone>();
//                ev.victoryZone = this;

//                Debug.Log("Victory Zone Triggered: Player Frozen and Text Shown.");
//            }
//        }
//    }
//}

//using Platformer.Gameplay;
//using TMPro;
//using UnityEngine;

//namespace Platformer.Mechanics
//{
//    public class VictoryZone : MonoBehaviour
//    {
//        public TMP_Text winText; // assign "You Win!" text in Inspector

//        void Awake()
//        {
//            if (winText != null)
//                winText.gameObject.SetActive(false); // hide at start
//        }

//        void OnTriggerEnter2D(Collider2D collider)
//        {
//            var p = collider.gameObject.GetComponent<PlayerController>();
//            if (p != null)
//            {
//                // Freeze player
//                p.enabled = false;
//                var rb = p.GetComponent<Rigidbody2D>();
//                if (rb != null) rb.linearVelocity = Vector2.zero;


//                // Show the "You Win!" text
//                if (winText != null)
//                    winText.gameObject.SetActive(true);

//                Debug.Log("Player entered victory zone!");
//            }
//            var ev = Schedule<PlayerEnteredVictoryZone>();
//            ev.victoryZone = this;
//        }
//    }
//}

using Platformer.Core;       // Required for Schedule<T>()
using Platformer.Gameplay;   // Required for PlayerEnteredVictoryZone
using TMPro;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class VictoryZone : MonoBehaviour
    {
        public TMP_Text winText; // assign "You Win!" text in Inspector

        void Awake()
        {
            // Hide Win Text at start
            if (winText != null)
                winText.gameObject.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            var player = collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                // Freeze player physics but allow Animator to run
                var rb = player.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;   // stop movement
                    rb.bodyType = RigidbodyType2D.Kinematic; ;        // freeze physics
                }

                // Play victory animation
                var anim = player.GetComponent<Animator>();
                if (anim != null)
                    anim.SetTrigger("Victory");   // make sure Animator has "Victory" trigger

                // Show the "You Win!" text
                if (winText != null)
                    winText.gameObject.SetActive(true);

                Debug.Log("Player entered victory zone!");
            }

            // Schedule the victory event
            var ev = Simulation.Schedule<PlayerEnteredVictoryZone>();
            ev.victoryZone = this;
        }
    }
}





//            }