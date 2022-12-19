using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowPlayerStats : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textHP;
    // Start is called before the first frame update
    private PlayerUnit player;
    void Start()
    {
        player = GetComponent<PlayerUnit>();
    }

    // Update is called once per frame
    void Update()
    {
        _textHP.text = player.Health.ToString() + " / " + player.MaxHealth.ToString() + " HP";
    }
}
