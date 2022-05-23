using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            var astronautsToExplore = astronauts.Where(x => x.Oxygen > 0).ToList();
            for (int i = 0; i < astronautsToExplore.Count; i++)
            {
                var astronaut = astronautsToExplore[i];
                foreach (var item in planet.Items)
                {
                    astronaut.Bag.Items.Add(item);
                    astronaut.Breath();
                    planet.Items.Remove(item);
                    if(astronaut.Oxygen <= 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}
