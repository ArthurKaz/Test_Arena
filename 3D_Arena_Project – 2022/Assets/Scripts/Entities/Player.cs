using Test_Project.Abstractions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test_Project.Entities
{
    public class Player : Entity, IActionAfterHitChance, IKillRewardReceiver, IUltimateUser
    {
        private const float CoefficientWorthToEnergy = 1.1f;
        private const float EnergyForReboundKill = 20;
        private const float MaxReboundChance = 1f;
        [Range(0.01f, 1f)] [SerializeField] private float minReboundChance = 0.05f;
        
        [SerializeField] private ReversibleFloat energy;
        private int _killedEnemies;
        public int KilledEnemies => _killedEnemies;

        public void Init()
        {
            energy.Init(0);
            health.Init(health.Max);
            Reset();
        }


        public float Energy => energy.Value;

        public void Exhaust()
        {
            energy.Reset();
        }
        public float Chance
        {
            get
            {
                //if you have full health, the chance is low, if you have little health, the chance is high
                float range = MaxReboundChance - minReboundChance;
                return MaxReboundChance - (HealthCoefficient * range);
            }
        }
        private float HealthCoefficient => Health / MaxHealth;

        public bool DidAfterHit()
        {
            return Random.value < Chance ;
        }

        public void ReceiveKillReward(float worth)
        {
            _killedEnemies++;
            ReceiveEnergy(worth * CoefficientWorthToEnergy);
        }

        private void ReceiveEnergy(float energy)
        {
            this.energy.Value += energy;
        }

        public void ReceiveRewardForReboundKill()
        {
            if (Random.value < 0.5f)
            {
                ReceiveEnergy(EnergyForReboundKill);
            }
            else
            {
                Heal();
            }
        }

        public IProgressBarValue GetEnergyProgress()
        {
            return energy;
        }

        public IProgressBarValue GetHealthProgress()
        {
            return health;
        }
    }
}