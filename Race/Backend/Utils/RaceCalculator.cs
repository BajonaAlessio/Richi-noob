using Backend.Models;

namespace Backend.Utils
{
    public static class RaceCalculator
    {
        private static readonly Random _random = new Random();

        public static double CalcolaTempo(Moto moto, Circuito circuito, int numeroCurve)
        {
            double tempoBase = circuito.Lunghezza * circuito.Giri * 10;

            double factorCurve = numeroCurve / 10.0;

            double controPotenza = moto.Potenza * factorCurve * 0.05;

            double bonusPotenza = (factorCurve < 1) ? -(moto.Potenza / 250.0) * 5 : 0;

            double bonusInclinazione = -(moto.Inclinazione / 60.0) * factorCurve * 5;

            double bonusGrip = -(moto.Grip / 100.0) * 3;

            double bonusAccelerazione = -(moto.Accelerazione / 100.0) * 4;

            double tempo = tempoBase + controPotenza + bonusPotenza + bonusInclinazione + bonusGrip + bonusAccelerazione;

            double variacion = tempo * 0.03;
            tempo += _random.NextDouble() * (variacion * 2) - variacion;

            return Math.Max(tempo, 1.0);
        }
    }
}