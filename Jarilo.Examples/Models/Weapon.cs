using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Examples.Models
{
    enum Weapon
    {
        None,

        [Value("katana", "Melee weapon. Powerful but slow.")]
        Katana,

        [Value("shuriken", "Ranged weapon. Good for harassing enemies.")]
        Shuriken,

        [Value("tanto", "Melee weapon. Fast but weak.")]
        Tanto
    }
}
