﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Lab_7.Blue_5;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }
            public void SetPlace(int place)
            {
                if (place <= 0 || _place > 0) return;
                _place = place;
            }
            public void Print()
            {
                Console.WriteLine($"Name: {Name}, Surname: {Surname}, place:{Place}");

            }


        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _sportsmenind;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            //{
            //    get
            //    {

            //        if (_sportsmen == null) return null;
            //        Sportsman[] copy = new Sportsman[_sportsmen.Length];
            //        Array.Copy(_sportsmen, copy, copy.Length);
            //        return copy;
            //    }
            //}
            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;

                    int sum = 0;

                    for (int i = 0; i < _sportsmen.Length; i++)
                    {

                        if (_sportsmen[i].Place == 1) sum += 5;
                        else if (_sportsmen[i].Place == 2) sum += 4;
                        else if (_sportsmen[i].Place == 3) sum += 3;
                        else if (_sportsmen[i].Place == 4) sum += 2;
                        else if (_sportsmen[i].Place == 5) sum += 1;
                    }
                    return sum;
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int maxi = 18;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i].Place != 0)
                        {
                            maxi = Math.Min(maxi, _sportsmen[i].Place);
                        }

                    }
                    return maxi;
                }
            }

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _sportsmenind = 0;
            }
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null) return;
                if (_sportsmenind < _sportsmen.Length)
                {
                    _sportsmen[_sportsmenind] = sportsman;
                    _sportsmenind++;
                }

            }
            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null || sportsmen == null || sportsmen.Length == 0 || _sportsmenind >= _sportsmen.Length)
                    return;

                int a = 0;
                while (_sportsmenind < _sportsmen.Length && a < sportsmen.Length)
                {
                    _sportsmen[_sportsmenind] = sportsmen[a];
                    _sportsmenind++;
                    a++;
                }

            }
            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        //if (teams[j + 1].SummaryScore > teams[j].SummaryScore)
                        //{
                        //    (teams[j + 1], teams[j]) = (teams[j], teams[j + 1]);
                        //}
                        if (teams[j + 1].SummaryScore > teams[j].SummaryScore)
                        {
                            (teams[j + 1], teams[j]) = (teams[j], teams[j + 1]);
                        }
                        else if (teams[j + 1].SummaryScore == teams[j].SummaryScore &&
                            teams[j + 1].TopPlace < teams[j].TopPlace)
                        {
                            Team tmp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = tmp;
                        }
                        //else if (teams[j + 1].SummaryScore == teams[j].SummaryScore &&
                        //    teams[j + 1].TopPlace > teams[j].TopPlace)
                        //{
                        //    Team tmp = teams[j ];
                        //    teams[j ] = teams[j+1];
                        //    teams[j+1] = tmp;
                        //}
                    }
                }
            }

            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null) return null;
                double  maxi = -1;
                int indmax = 0;
                for (int i = 1; i < teams.Length; i++)
                {
                    if (teams[i] == null) continue;
                    if (teams[i].GetTeamStrength() > maxi)
                    {
                        maxi = teams[i].GetTeamStrength();
                        indmax = i;
                    }
                }
                return teams[indmax];
                
            }
            public void Print()
            {
                Console.WriteLine(_name);
                for (int k = 0; k < _sportsmenind; k++)
                    _sportsmen[k].Print();
                Console.WriteLine();
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {

            }
            protected override double GetTeamStrength()
            {
                int countp = 0;
                int count = 0;
                foreach(Sportsman man in this.Sportsmen)
                {
                    if (man == null) continue;
                    countp += man.Place;
                    count++;
                }
                return 100 / (countp / count);
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {

            }
            protected override double GetTeamStrength()
            {
                int countp = 0;
                int ch = 0;
                int pr = 1;
                foreach (Sportsman woman in this.Sportsmen)
                {
                    if (woman == null) continue;
                    countp += woman.Place;
                    ch++;
                    pr *= woman.Place;
                }
                return 100 * countp * ch / pr;
            }
        }
    }
}
