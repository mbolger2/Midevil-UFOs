using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;


namespace TD
{
    public class ThinkingPlaceable : Placeable
    {
        [HideInInspector] public States state = States.Dragged;
        public enum States
        {
            Dragged,    // When the player is dragging the tower onto the field
            Idle,       // State when not attcking
            Seeking,    // The units chasing target
            Attacking,  // Attack cycle animation
            Dead,       // The player destroys the building
        }

        [HideInInspector] public AttackType attackType;
        public enum AttackType
        {
            None,
            Melee,
            ShortRange,
            MedRange,
            LongRange,
        }

        [HideInInspector] public ThinkingPlaceable target;
        // [HideInInspector] public HealthBar healthBar;

        [HideInInspector] public float hitPoints;
        [HideInInspector] public float attackRange;
        [HideInInspector] public float attackRatio;
        [HideInInspector] public float lastBlowTime = -1000f;
        [HideInInspector] public float damage;
        [HideInInspector] public AudioClip attackAudioClip;

        [HideInInspector] public float timeToActNext = 0f;

        // Inspector References
        [Header("Projectile for Ranged")]
        public GameObject projectilePrefab;
        public Transform projectileSpawnPoint;

        private Projectile projectile;
        protected AudioSource audioSource;

        public UnityAction<ThinkingPlaceable> OnDealDamage, OnProjectileFired;

        public virtual void SetTarget(ThinkingPlaceable t)
        {
            target = t;
            t.onDie += TargetIsDead;
        }

        public virtual void StartAttack()
        {
            state = States.Attacking;
        }

        public virtual void DealBlow()
        {
            lastBlowTime = Time.time;
        }    

        // Animation event hooks
        public void DealDamage()
        {
            if (OnDealDamage != null)
            {
                OnDealDamage(this);
            }
        }

        public void FireProjectile()
        {
            if (OnProjectileFired != null)
            { 
                OnProjectileFired(this); 
            }
        }

        public virtual void Seek()
        {
            state = States.Seeking;
        }

        protected void TargetIsDead(Placeable p)
        {
            state = States.Idle;

            target.onDie -= TargetIsDead;

            timeToActNext = lastBlowTime + attackRatio;
        }

        public bool IsTargetInRange()
        {
            return (transform.position - target.transform.position).sqrMagnitude <= attackRange * attackRange;
        }

        public virtual void Stop()
        {
            state = States.Idle;
        }

        protected virtual void Die()
        {
            state = States.Dead;

            if (onDie != null)
            {  
                onDie(this);
            }
        }
    }
}

