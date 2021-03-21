using Fishs.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fishs
{
    public delegate void OceanUpdated(Fish[] fishs, List<Enemy> enemies);

    public class Ocean
    {
        public event OceanUpdated OceanUpdatedEvent;

        Fish[] fishs = null;
        List<Enemy> enemies = null;
        Random randomGenerator;
        protected double MAX_WIDTH;
        protected double MAX_HEIGHT;

        public Ocean(int nbfishs, double width, double height)
        {
            MAX_HEIGHT = height;
            MAX_WIDTH = width;
            fishs = new Fish[nbfishs];
            randomGenerator = new Random();
            enemies = new List<Enemy>();

            for (int i = 0; i < nbfishs; i++)
            {
                fishs[i] = new Fish(new Point(randomGenerator.NextDouble() * MAX_WIDTH, randomGenerator.NextDouble() * MAX_HEIGHT), randomGenerator.NextDouble() * 2 * Math.PI);
            }
        }

        public void AddEnemmy(Point point, double radius)
        {
            enemies.Add(new Enemy(point, radius));
        }

        private void UpdateEnemies()
        {
            enemies.ForEach(a => a.Update());
            enemies.RemoveAll(x => x.Dead());
        }

        private void UpdateFishs()
        {
            foreach (var f in fishs)
            {
                f.Update(fishs, enemies, MAX_WIDTH, MAX_HEIGHT);
            }
        }
        public void UpdateOcean()
        {
            UpdateEnemies();
            UpdateFishs();
            if (OceanUpdatedEvent != null)
                OceanUpdatedEvent(fishs, enemies);
        }
    }
}
