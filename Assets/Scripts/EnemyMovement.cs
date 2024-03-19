using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyMovement : MonoBehaviourPun
{
    public float moveSpeed = 2.0f;
    private GameObject targetPlayer;

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            // Eðer bu istemci Master Client deðilse, hareketi güncelleme.
            return;
        }

        if (targetPlayer == null)
        {
            FindTargetPlayer();
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    void FindTargetPlayer()
    {
        // Mevcut oyuncularý bul
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            // Rastgele bir oyuncuyu hedef al
            targetPlayer = players[Random.Range(0, players.Length)];
        }
    }

    void MoveTowardsTarget()
    {
        // Hedef oyuncuya doðru hareket et
        if (targetPlayer != null)
        {
            Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

}



