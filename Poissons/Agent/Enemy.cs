using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fishs.Agent
{
    public class Enemy :AgentBase
    {
        protected int m_TimeLive = 500;
        private double m_Radius = 0;

        public double Radius { get => m_Radius; set => m_Radius = value; }

        public Enemy(Point _point, double _radius) : base(_point)
        {
            m_Radius = _radius;
        }

        public void Update()
        {
            m_TimeLive--;
        }

        public bool Dead()
        {
            return m_TimeLive <= 0;
        }
    }
}
