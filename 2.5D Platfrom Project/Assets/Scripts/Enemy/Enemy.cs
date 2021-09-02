using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private string currentState = "IdleState";
    private Transform target;
    public float chaseRange = 5, attackRange = 2, followRange, enemySpeed = 3;
    public Animator animator;
    public Slider enemyHealthBar;
    public Gradient gradient;
    public Image fill;
    public GameObject canvasHealth;

    public int enemyHP, enemyMaxHP;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyHP = enemyMaxHP;
        enemyHealthBar.maxValue = enemyHP;
        enemyHealthBar.value = enemyHP;
        fill.color = gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.gameOver)
        {
            animator.ResetTrigger("chase");
            animator.SetBool("isAttacking", false);
            animator.SetBool("gameOver", true);
        }
        float distance = Vector3.Distance(transform.position, target.position);

        #region IdleRunAttackState

        if (currentState == "IdleState")
        {
            // karakter ve düþman uzaklýðý belirtilen uzaklýktan küçük ise "chase durumu" aktif olur. Yani karakter ve düþman birbirine yakýn demektir.
            if (distance < chaseRange)
                currentState = "ChaseState";
        }

        else if (currentState == "ChaseState" && (distance < followRange)) // "chase durumu" aktif ve düþman-oyuncu uzaklýðý istenilen aralýkta ise bu koþul aktif olur
        {
            animator.SetTrigger("chase"); // chase yani düþman koþma animasyonu aktifleþtirildi.
            animator.SetBool("isAttacking", false);

            if (distance < attackRange)
            {
                currentState = "AttackState";
            }

            if (target.position.x > transform.position.x) // target'ýn x deðeri(yatay eksende) düþmandan büyükse düþman solda karakter(target) saðda demektir.
            {
                // düþman solda karakter(target) saðda olduðundan düþmanýn yönü sað'a doðru olmalý.
                transform.Translate(transform.right * enemySpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else // target x deðeri düþman'dan küçükse düþman saðda karakter(target) solda demektir.
            {
                // düþman saðda karakter(target) solda olduðundan düþmanýn yönü sol'a doðru olmalý.
                transform.Translate(-transform.right * enemySpeed * Time.deltaTime); // transform.left olmadýðýnda "-transform.right" yaptýk.
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        else if (currentState == "AttackState" && (distance < attackRange))
        {
            animator.SetBool("isAttacking", true);
        }

        else
        {
            animator.ResetTrigger("chase");
            currentState = "IdleState";
            animator.SetTrigger("idle");
        }
        #endregion
    }

    public void TakeDamage(int damage)
    {
        enemyHP -= damage;
        enemyHealthBar.value = enemyHP;
        fill.color = gradient.Evaluate(enemyHealthBar.normalizedValue);
        currentState = "ChaseState";

        if (enemyHP < 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        animator.SetTrigger("isDead");
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        canvasHealth.SetActive(false);
        this.enabled = false;
        Destroy(gameObject, 3.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathPoint")
        {
            Destroy(gameObject, 1.5f);
        }
    }
}
