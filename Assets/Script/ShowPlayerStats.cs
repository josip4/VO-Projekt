using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowPlayerStats : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textHP;
    // Start is called before the first frame update
    public PlayerUnit player;

    // Update is called once per frame
    void Update()
    {
      if (player == null)
        _textHP.text ="0 / 100 HP";
      else
        _textHP.text = player.Health.ToString() + " / " + player.MaxHealth.ToString() + " HP";

    }

}
