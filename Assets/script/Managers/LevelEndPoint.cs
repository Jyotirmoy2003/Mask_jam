using UnityEngine;

public class LevelEndPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player"))
        {
            Debug.Log("player Reached to checkpoint");
            GameManager.Instance.OnLevelEnd();
        }
    }
}
