using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ
{
    [AddComponentMenu("Combat System/Health Handler")]
    public class HealthHandler : MonoBehaviour
    {
        // this will handle the health of any character.

        [SerializeField, Tooltip("Set this value to either a Game Architecture Variable, or a constant if you want a specific enemy")]
        public FloatReference maxHealthRef;

        [SerializeField, Tooltip("Health Regeneration in Health Per Seconds. Set this value to 0 for no health regen, or give it a value, or use a global variable.")]
        public FloatReference healthRegenerationRateRef;

        [SerializeField, Tooltip("Delay in seconds before health regeneration begins. Set it to -1 to stop health regeneration altogether.")]
        public FloatReference healthRegenerationDelayRef;


        [Tooltip("Returns a percentage of the current health out of max health.")]
        public UnityEvent<float> OnCurrentHealthChanged;
        [Tooltip("This took damage? What happens on damage?")]
        public UnityEvent OnTakeDamage;
        [Tooltip("This now has 0 health, what do?")]
        public UnityEvent OnDeath;



        private float currentHealth { get { return currentHealth; } set { CurrentHealthChanged(); currentHealth = value ; } }

        private bool isDead = false;
        private bool canRegenerateHealth = false;
        private bool regeneratingHealth = false;
        private bool healthRegenIsDelayed = false;

        private float maxHealth;
        private float healthRegen;
        private float regenDelay;

        private float lastRecordedHealth;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            isDead = false;
            currentHealth = maxHealthRef.Value;
            maxHealth = maxHealthRef.Value;
            healthRegen = healthRegenerationRateRef.Value;
            regenDelay = healthRegenerationDelayRef.Value;
            regeneratingHealth = false;
            healthRegenIsDelayed = false;

            canRegenerateHealth = regenDelay == -1f;
        }

        private void Update()
        {
            if (isDead) return;

            if(regeneratingHealth && currentHealth < maxHealth)
            {
                currentHealth += healthRegen * Time.deltaTime;

                if(currentHealth >= maxHealth)
                {
                    currentHealth = maxHealth;
                    regeneratingHealth = false;
                }
            }
        }

        private void TookDamage()
        {
            if (isDead) return;
            // we won't regenerate health if we can't.
            if (!canRegenerateHealth) return;

            // stop the current health regeneration.
            if(healthRegenIsDelayed == true)
            {
                StopAllCoroutines();
            }


            StartCoroutine(HealthRegenerationDelay());

        }

        private IEnumerator HealthRegenerationDelay()
        {
            healthRegenIsDelayed = true;
            regeneratingHealth = false;

            var waitForEndOfFrame = new WaitForEndOfFrame();

            var timeElapsed = 0f;

            while(timeElapsed < regenDelay)
            {
                timeElapsed += regenDelay * Time.deltaTime;
                yield return waitForEndOfFrame;
            }


            healthRegenIsDelayed = false;
            regeneratingHealth = true;
        }


        public void TakeDamage(float damage)
        {
            if(isDead) return;

            currentHealth -= damage;

            OnTakeDamage?.Invoke();
            TookDamage();
            if(currentHealth <= 0) 
            {
                currentHealth = 0;
                isDead = true;
                OnDeath?.Invoke();

                this.enabled = false;
            }
        }


        private void CurrentHealthChanged()
        {
            OnCurrentHealthChanged?.Invoke(currentHealth / maxHealth);
        }
    }
}
