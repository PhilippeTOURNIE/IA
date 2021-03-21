using System;
using System.Windows;

namespace Fishs
{
    public class AgentBase
    {
        protected Point m_Point;
        public Point Pos { get => m_Point; set => m_Point = value; }


        public AgentBase()
        {

        }

        public AgentBase(Point _point)
        {
            m_Point = _point;
        }

        public double DistanceTo(AgentBase _agent)
        {
            return Math.Sqrt((_agent.m_Point.X - m_Point.X) * (_agent.m_Point.X - m_Point.X)
                  + (_agent.m_Point.Y - m_Point.Y) * (_agent.m_Point.Y - m_Point.Y));
        }

        public double SquareDistancTo(AgentBase _agent)
        {
            return (_agent.m_Point.X - m_Point.X) * (_agent.m_Point.X - m_Point.X)
                  + (_agent.m_Point.Y - m_Point.Y) * (_agent.m_Point.Y - m_Point.Y);
        }
    }
}
