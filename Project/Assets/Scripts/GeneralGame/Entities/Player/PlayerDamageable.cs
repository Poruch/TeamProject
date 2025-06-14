using Assets.Scripts.GeneralGame.Entities.StatsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    internal class PlayerDamageable : IDamageable
    {
        public override void Damage(DamageArgs damageArgs)
        {
            base.Damage(damageArgs);
            if(Shield.isEmpty())
                Hp.Reduce(damageArgs.Damage);
            else
                Shield.Reduce(damageArgs.Damage);
        }


    }
}
