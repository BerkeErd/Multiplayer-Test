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
            // E�er bu istemci Master Client de�ilse, hareketi g�ncelleme.
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
        // Mevcut oyuncular� bul
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            // Rastgele bir oyuncuyu hedef al
            targetPlayer = players[Random.Range(0, players.Length)];
        }
    }

    void MoveTowardsTarget()
    {
        // Hedef oyuncuya do�ru hareket et
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



