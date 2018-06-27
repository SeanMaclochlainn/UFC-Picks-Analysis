using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Entities
{
    public class Entity
    {
        protected FightPicksContext context;

        public Entity() : this(new FightPicksContext()) { }

        public Entity(FightPicksContext context)
        {
            this.context = context;
        }
    }
}
