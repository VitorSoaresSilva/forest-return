using UnityEngine;

namespace WaveManager
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public int[] amountOfEnemiesPerWave;
        private int _currentMaxEnemies;
        private int _currentEnemiesAlive;
        private int _currentWave = -1;
        public void EnemyDied()
        {
            if (--_currentEnemiesAlive <= 0)
            {
                SpawnNextWave();
            }
        }

        private void SpawnNextWave()
        {
            if (++_currentWave < amountOfEnemiesPerWave.Length)
            {
                Debug.Log("Spawn Enemies");
            }
        }
    }
}