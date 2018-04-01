using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class Process
    {

        public Process()
        {
            id = -1;
            arriveTime = 0;
            burstTime = 0;
            assigned = false;
            finished = false;
            priority = -1;
        }

        public Process(int id, double arrive, double burst)
        {
            this.id = id;
            arriveTime = arrive;
            burstTime = burst;
            assigned = false;
            finished = false;
            priority = -1;
        }

        public Process(int id, double arrive, double burst, bool pAssigned, bool pFinished)
        {
            this.id = id;
            arriveTime = arrive;
            burstTime = burst;
            assigned = pAssigned;
            finished = pFinished;
            priority = -1;
        }

        public Process(int id, double arrive, double burst, double p, 
            bool pAssigned, bool pFinished)
        {
            this.id = id;
            arriveTime = arrive;
            burstTime = burst;
            assigned = pAssigned;
            finished = pFinished;
            priority = p;
        }

        public int CompareArriveTime(Process p)
        {
            if (this.arriveTime < p.arriveTime)
                return -1;
            else if (this.arriveTime == p.arriveTime)
                return 0;
            else
                return 1;
        }

        public int CompareBurstTime(Process p)
        {
            if (this.burstTime < p.burstTime)
                return -1;
            else if (this.burstTime == p.burstTime)
                return 0;
            else
                return 1;
        }

        public bool CheckPriority(Process p)
        {
            if (this.priority < p.priority)
                return false;
            else
                return true;
        }

        public double GetBurstTime()
        {
            return burstTime;
        }

        public double GetArriveTime()
        {
            return arriveTime;
        }

        public int GetID()
        {
            return id;
        }

        public bool IsAssigned()
        {
            return assigned;
        }

        public bool IsFinished()
        {
            return finished;
        }

        public void MarkAssigned()
        {
            assigned = true;
        }

        public void MarkFinished()
        {
            finished = true;
        }

        public void SetBurstTime(double burst)
        {
            burstTime = burst;
        }

        private int id;
        private double arriveTime, burstTime, priority;
        private bool assigned, finished;
    };
}
