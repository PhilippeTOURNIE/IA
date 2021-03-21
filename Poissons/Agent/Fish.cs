using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fishs.Agent
{
    public class Fish : AgentBase
    {
        protected const double STEP = 3;
        protected const double DISTANCE_MIN = 5;
        protected const double SQUARE_DISTANCE_MIN = 5;
        protected const double DISTANCE_MAX = 40;
        protected const double DISTANCE_MAX_SQUARE = 1600;


        public Point Speed;

        public Fish(Point _point, double _dir) : base(_point)
        {
            Speed.X = Math.Cos(_dir);
            Speed.Y = Math.Sin(_dir);
        }

        public void UpdatePosition()
        {
            m_Point.X += STEP * Speed.X;
            m_Point.Y += STEP * Speed.Y;
        }

        public bool Near(Fish _fish)
        {
            double squareDistance = SquareDistancTo(_fish);
            return squareDistance < DISTANCE_MAX_SQUARE && squareDistance > SQUARE_DISTANCE_MIN;
        }

        public double DistanceToWall(double _wallXMin, double _wallYMin, double _wallXMax, double _wallYMax)
        {
            double min = double.MaxValue;
            min = Math.Min(min, m_Point.X - _wallXMin);
            min = Math.Min(min, m_Point.Y - _wallYMin);
            min = Math.Min(min, _wallXMax - m_Point.X);
            min = Math.Min(min, _wallYMax - m_Point.Y);
            return min;
        }

        protected void Normalize()
        {
            double speedLength = Math.Sqrt(Speed.X * Speed.X + Speed.Y * Speed.Y);
            Speed.X /= speedLength;
            Speed.Y /= speedLength;
        }

        public bool AvoidWalls(double _wallXMin, double _wallYMin, double _wallXMax, double _wallYMax)
        {
            // S'arrete au mur
            if (m_Point.X < _wallXMin)
            {
                m_Point.X = _wallXMin;
            }
            if (m_Point.Y < -_wallYMin)
            {
                m_Point.Y = _wallYMin;
            }
            if (m_Point.X > _wallXMax)
            {
                m_Point.X = _wallXMax;
            }
            if (m_Point.Y > _wallYMax)
            {
                m_Point.Y = _wallYMax;
            }

            // changement de direction
            double distance = DistanceToWall(_wallXMin, _wallYMin, _wallXMax, _wallYMax);
            if (distance < DISTANCE_MAX)
            {
                if (distance == m_Point.X - _wallXMin)
                {
                    Speed.X += 0.3;
                }

                else if (distance == (m_Point.Y - _wallYMin))
                {
                    Speed.Y += 0.3;

                }
                else if (distance == (_wallXMax - m_Point.X))
                {
                    Speed.X -= 0.3;
                }
                else if (distance == (_wallYMax - m_Point.Y))
                {
                    Speed.Y -= 0.3;
                }

                Normalize();
                return true;
            }

            return false;
        }


        public bool AvoidEnemy(List<Enemy> enemies)
        {
            var nearestEnemy = enemies.Where(x => SquareDistancTo(x) <2* x.Radius * x.Radius).FirstOrDefault();

            if (nearestEnemy != null)
            {
                double distanceToEnemy = DistanceTo(nearestEnemy);
                double diffX = (nearestEnemy.Pos.X - m_Point.X) / distanceToEnemy;
                double diffY = (nearestEnemy.Pos.Y - m_Point.Y) / distanceToEnemy;
                Speed.X = Speed.X - diffX / 2;
                Speed.Y = Speed.Y - diffY / 2;
                Normalize();
                return true;
            }

            return false;
        }

        public bool AvoidFish(Fish fish)
        {
            double squareDistanceToFish = SquareDistancTo(fish);
            
            if (squareDistanceToFish< SQUARE_DISTANCE_MIN)
            {
                double diffX = (fish.Pos.X - Pos.X) / Math.Sqrt(squareDistanceToFish);
                double diffY = (fish.Pos.Y - Pos.Y) / Math.Sqrt(squareDistanceToFish);
                Speed.X = Speed.X- diffX / 4;
                Speed.Y = Speed.Y -diffY / 4;
                Normalize();
                return true;
            }

            return false;
        }

        internal void ComputeAverageDirection(Fish[] fishs)
        {
            List<Fish> fishsNearest = fishs.Where(x => Near(x)).ToList();

            var nb = fishsNearest.Count;
            if (nb>0)
            {
                double speedXTotal = 0;
                double speedYTotal = 0;

                foreach (var f in fishsNearest)
                {
                    speedXTotal += f.Speed.X;
                    speedYTotal += f.Speed.Y;
                }

                Speed.X = (speedXTotal / nb + Speed.X) / 2;
                Speed.Y = (speedYTotal / nb + Speed.Y) / 2;
                Normalize();
            }
        }

        internal void Update(Fish[] fishs, List<Enemy> enemies, double maxWidth, double maxHeigth)
        {
            if (!AvoidWalls(0, 0, maxWidth, maxHeigth))
            {
                if (!AvoidEnemy(enemies))
                {
                    double squareDistanceMin = fishs.Where(x => !x.Equals(this)).Min(x => x.SquareDistancTo(this));

                    if(!AvoidFish(fishs.Where(x=> x.SquareDistancTo(this) == squareDistanceMin).FirstOrDefault()))
                    {
                        ComputeAverageDirection(fishs);
                    }
                }
            }

            UpdatePosition();
        }
    }
}

