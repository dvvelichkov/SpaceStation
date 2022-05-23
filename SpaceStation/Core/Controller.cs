using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SpaceStation.Models.Mission;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronautRepository;
        private PlanetRepository planetRepository;
        private Mission mission;
        private int exploredPlanetsCount = 0;


        public Controller()
        {
            this.astronautRepository = new AstronautRepository();
            this.planetRepository = new PlanetRepository();
            this.mission = new Mission();
            
        }
        public string AddAstronaut(string type, string astronautName)
        {
            if(type == "Biologist")
            {
                IAstronaut astronaut = new Biologist(astronautName);
                astronautRepository.Add(astronaut);
                return string.Format(OutputMessages.AstronautAdded, type, astronautName);
            }
            else if(type == "Geodesist")
            {
                IAstronaut astronaut = new Geodesist(astronautName);
                astronautRepository.Add(astronaut);
                return string.Format(OutputMessages.AstronautAdded, type, astronautName);
            }
            else if(type == "Meteorologist")
            {
                IAstronaut astronaut = new Meteorologist(astronautName);
                astronautRepository.Add(astronaut);
                return string.Format(OutputMessages.AstronautAdded, type, astronautName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }
            planetRepository.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var astronautsForExploring = astronautRepository.Models.Where(x => x.Oxygen > 60).ToList();
            var planetForExploring = planetRepository.Models.FirstOrDefault(x => x.Name == planetName);
            Mission mission = new Mission();
            mission.Explore(planetForExploring, astronautsForExploring);
            if (astronautsForExploring.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }
            else
            {
                var deadAstronauts = astronautsForExploring.Where(x => x.Oxygen <= 0).ToList();
                int deadAstronautsCount = deadAstronauts.Count;
                exploredPlanetsCount++;
                return string.Format(OutputMessages.PlanetExplored, planetName, deadAstronautsCount);
            }
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{exploredPlanetsCount} planets were explored! \r\n" +
                $"Astronauts info:");
            sb.AppendLine();
            foreach (var astronaut in astronautRepository.Models)
            {
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                if(astronaut.Bag.Items.Count == 0)
                {
                    sb.AppendLine("Bag items: none");
                }
                else
                {
                    sb.AppendLine($"Bag items:");
                    foreach (var item in astronaut.Bag.Items)
                    {
                        sb.Append(item );
                    }
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        public string RetireAstronaut(string astronautName)
        {
            var astronautForRetirement = astronautRepository.FindByName(astronautName);
            if(astronautForRetirement == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }
            else
            {
                astronautRepository.Remove(astronautForRetirement);
                return string.Format(OutputMessages.AstronautRetired, astronautName);
            }
        }
    }
}
