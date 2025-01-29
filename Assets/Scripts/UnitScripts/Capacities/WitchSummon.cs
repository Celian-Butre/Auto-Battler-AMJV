using UnityEngine;

public class WitchSummon : BaseCapacity
{
   [SerializeField] private GameObject summonUnit;

   protected override bool CapacityCall()
   {
      print("SUMMON");
      print(Mana);
      Instantiate(summonUnit, transform.position, Quaternion.identity);
      return true;
   }
}
