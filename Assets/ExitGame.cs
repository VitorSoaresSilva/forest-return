using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Utilities;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.ChangeScene(Enums.Scenes.EndGame);
    }
}
