﻿using System;

namespace KelpNet.Optimizers
{
    [Serializable]
    public class MomentumSGD : Optimizer
    {
        public double LearningRate;
        public double Momentum;

        public MomentumSGD( double learningRate = 0.01, double momentum = 0.9)
        {
            this.LearningRate = learningRate;
            this.Momentum = momentum;
        }

        public override void Initilise(FunctionParameter[] functionParameters)
        {
            this.OptimizerParameters = new OptimizerParameter[functionParameters.Length];

            for (int i = 0; i < this.OptimizerParameters.Length; i++)
            {
                this.OptimizerParameters[i] = new MomentumSGDParameter(functionParameters[i], this);
            }
        }
    }

    [Serializable]
    class MomentumSGDParameter : OptimizerParameter
    {
        private readonly MomentumSGD optimiser;
        private readonly double[] v;

        public MomentumSGDParameter(FunctionParameter functionParameter, MomentumSGD optimiser) : base(functionParameter)
        {
            this.v = new double[functionParameter.Length];
            this.optimiser = optimiser;
        }

        public override void UpdateFunctionParameters()
        {
            for (int i = 0; i < this.FunctionParameter.Length; i++)
            {
                this.v[i] *= this.optimiser.Momentum;
                this.v[i] -= this.optimiser.LearningRate * this.FunctionParameter.Grad.Data[i];

                this.FunctionParameter.Param.Data[i] += this.v[i];
            }
        }
    }

}
