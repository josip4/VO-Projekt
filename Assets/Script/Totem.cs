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

	private ParticleSystem _aura = null;
	private bool _playerInRange = false;
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
	Attack<BaseUnit>(null);
	}
	private void CheckPlayerInRange()
	{
	PlayerUnit player = FindObjectOfType<PlayerUnit>();
	float distance = Vector3.Distance (player.transform.position, transform.position);
	_playerInRange = distance <= _attackRange ? true : false;
	if (_aura is null) return;
	if (_playerInRange) _aura.Play();
	else _aura.Pause();
	}
	IEnumerator Spawn()
	{
	_spawning = true;
	yield return new WaitForSeconds(1 / _attackSpeed);
	Vector3 randomSpawnPosition = new Vector3(Random.Range(6f, 10f), 0.9f, Random.Range(-18f, -20f));
	Quaternion randomRotation = Random.rotation;
	randomRotation.x = 0;
	randomRotation.z = 0;
	Instantiate(_monster, randomSpawnPosition, randomRotation);
	// yield return new WaitForSeconds(7f);
	_animator.SetBool("PlayerInRange", _playerInRange);
	}

	public override void Attack<T>(T target)
	{
		_animator.SetBool("PlayerInRange", _playerInRange);
		if (_aura is null)
		{
			Vector3 auraPos = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
			_aura = Instantiate(_auraVFX, auraPos, Quaternion.identity);
		}    
		if (_spawning) return;
		StartCoroutine(Spawn());
	}
}
