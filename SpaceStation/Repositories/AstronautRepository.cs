using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private readonly List<IAstronaut> models;

        public AstronautRepository()
        {
            this.models = new List<IAstronaut>();
        }
        public IReadOnlyCollection<IAstronaut> Models
        {
            get { return this.models.ToList(); }
        }

        public void Add(IAstronaut astronaut)
        {
            models.Add(astronaut);
        }

        public IAstronaut FindByName(string name)
        {
            var astronautFound = models.FirstOrDefault(x => x.Name == name);
            if(astronautFound != null)
            {
                return astronautFound;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(IAstronaut astronaut)
        {
            if(models.Contains(astronaut))
            {
                models.Remove(astronaut);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
