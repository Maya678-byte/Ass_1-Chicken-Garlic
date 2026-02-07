using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;

        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
            }
        }

        void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
        }

        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    // Only handle collisions with something tagged as Enemy or Boss
        //    if (other.CompareTag("Enemy"))
        //    {
        //        // Normal enemy dies immediately
        //        Destroy(other.gameObject);

        //        // Destroy the projectile
        //        Destroy(gameObject);
        //    }
        //    else if (other.CompareTag("Boss"))
        //    {
        //        // Boss has Health component
        //        var health = other.GetComponent<Health>();
        //        if (health != null)
        //        {
        //            health.Decrement(); // deal 1 damage
        //        }

        //        // Destroy projectile
        //        Destroy(gameObject);

        //        // Optional: destroy boss if HP reaches 0
        //        if (health != null && !health.IsAlive)
        //        {
        //            Destroy(other.gameObject);
        //        }
        //    }
        //}




    }
}