using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> models;

        public PlanetRepository()
        {
            this.models = new List<IPlanet>();
        }
        public IReadOnlyCollection<IPlanet> Models
        {
            get { return this.models.ToList(); }
        }

        public void Add(IPlanet planet)
        {
            models.Add(planet);
        }

        public IPlanet FindByName(string name)
        {
            var planetFound = models.FirstOrDefault(x => x.Name == name);
            if (planetFound != null)
            {
                return planetFound;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(IPlanet planet)
        {
            if (models.Contains(planet))
            {
                models.Remove(planet);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
