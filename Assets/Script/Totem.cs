using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : BaseUnit
{
  private Animator _animator;
  [SerializeField]
  private Devil _monster;
  [SerializeField]
  private ParticleSystem _auraVFX;

  private GameObject aura = null;
  private bool _playerInRange = false;
  private bool auraOn = false;
  private bool _spawning = false;
  
  void Start()
  {
    _animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    CheckPlayerInRange();
    if (!_playerInRange) return;
    if (_spawning) return;
    _animator.SetBool("PlayerInRange", _playerInRange);
    StartCoroutine(Spawn());

    if (auraOn) return;
    if (aura is null)
    {
      auraOn = true;
      Vector3 auraPos = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
      // aura = Instantiate(_auraVFX, auraPos, Quaternion.identity);
      return;
    }
    // aura.Play();
    auraOn = true;
    
  }
  private void CheckPlayerInRange()
  {
    PlayerUnit player = FindObjectOfType<PlayerUnit>();
    float distance = Vector3.Distance (player.transform.position, transform.position);
    _playerInRange = distance <= _attackRange ? true : false;
  }

  IEnumerator Spawn()
  {
    _spawning = true;
    yield return new WaitForSeconds(1 / _attackSpeed);
    Vector3 randomSpawnPosition = new Vector3(Random.Range(6, 10), 0.7f, Random.Range(-18, -20));
    Instantiate(_monster, randomSpawnPosition, Quaternion.identity);
    // yield return new WaitForSeconds(7f);
    _animator.SetBool("PlayerInRange", _playerInRange);
    if (aura is not null){
      // aura.Stop();
      auraOn = false;
    }  
    _spawning = false;
  }

  public override void Attack(BaseUnit target)
  {
      throw new System.NotImplementedException();
  }
}
