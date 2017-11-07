using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Examples.Models
{
    enum Item
    {
        None,

        [Value("sleeping-dart", "Sleeping dart.")]
        SleepingDart,

        [Value("poison-dart", "Poison dart.")]
        PoisonDart,

        [Value("healing-potion", "Healing potion.")]
        HealingPotion,

        [Value("stamina-potion", "Stamina potion.")]
        StaminaPotion
    }
}
