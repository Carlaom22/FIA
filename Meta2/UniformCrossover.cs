using GeneticSharp.Domain.Chromosomes;
using System;
using System.Linq;
using UnityEngine;
using GeneticSharp.Domain.Randomizations;
using System.Collections.Generic;
using GeneticSharp.Domain.Crossovers;

namespace GeneticSharp.Runner.UnityApp.Commons
{
    public class UniformCrossover : ICrossover
    {




        public int ParentsNumber { get; private set; }

        public int ChildrenNumber { get; private set; }

        public int MinChromosomeLength { get; private set; }

        public bool IsOrdered { get; private set; } // indicating whether the operator is ordered (if can keep the chromosome order).

        protected float crossoverProbability;


        public UniformCrossover(float crossoverProbability) : this(2, 2, 2, true)
        {
            this.crossoverProbability = crossoverProbability;
        }

        public UniformCrossover(int parentsNumber, int offSpringNumber, int minChromosomeLength, bool isOrdered)
        {
            ParentsNumber = parentsNumber;
            ChildrenNumber = offSpringNumber;
            MinChromosomeLength = minChromosomeLength;
            IsOrdered = isOrdered;
        }

        public IList<IChromosome> Cross(IList<IChromosome> parents)
        {
            IChromosome parent1 = parents[0];
            IChromosome parent2 = parents[1];
            IChromosome offspring1 = parent1.Clone();
            IChromosome offspring2 = parent2.Clone();

            double[] mask = new double[parent1.Length];
            for (int i = 0; i < parent1.Length; i++)
            {
                mask[i] = RandomizationProvider.Current.GetDouble(0, 1);
            }

            if (RandomizationProvider.Current.GetDouble() <= crossoverProbability)
            {
                for (int i = 0; i < parent1.Length; i++)
                {
                    if (mask[i] >= 0.5)
                    {
                        offspring1.ReplaceGene(i, parent2.GetGene(i));
                        offspring2.ReplaceGene(i, parent1.GetGene(i));
                    }
                }
            }



            return new List<IChromosome> { offspring1, offspring2 };

        }
    }
}