using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintPlanning.Base;

namespace SprintPlanning
{
    public class SprintSolution : ISolution
    {
        List<Ticket> loadedContent;
        public List<Ticket> LoadedContent
        {
            get { return loadedContent; }
            set { loadedContent = value; }
        }

        public double Weight
        {
            get { return LoadedContent.Sum(x => x.Priority); }
        }

        public double Value
        {
            get { return LoadedContent.Sum(x => x.StoryPoint); }
        }

        public SprintSolution()
        {
            loadedContent = new List<Ticket>();
        }

        public SprintSolution(SprintSolution _solution)
        {
            loadedContent = new List<Ticket>();
            loadedContent.AddRange(_solution.loadedContent);
        }

        public override string ToString()
        {
            String res = "StoryPoint : " + Value + " - Priority : " + Weight + "\n";
            res += "Loaded : ";
            res += String.Join(" - ", loadedContent);
            return res;
        }

        public override bool Equals(object _object)
        {
            SprintSolution solution = (SprintSolution)_object;
            if (solution.loadedContent.Count != loadedContent.Count || solution.Value != Value || solution.Weight != Weight)
            {
                return false;
            }
            else
            {
                foreach (Ticket planning in loadedContent)
                {
                    if (!solution.loadedContent.Contains(planning))
                    {
                        return false;
                    }
                }

            }
            return true;
        }
    }
}
