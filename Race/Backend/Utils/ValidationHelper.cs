using Backend.Models;

namespace Backend.Utils
{
    public class ValidationHelper
    {
        public static bool IsNotNullOrWhiteSpace(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsPositiveDecimal(decimal value)
        {
            return value > 0;
        }

        public static bool IsPositiveInt(int value)
        {
            return value > 0;
        }

        public static void PilotaValidation(Pilota pilota)
        {
            if (pilota == null)
                throw new ArgumentNullException(nameof(pilota), "il pilota non può essere null");
            if (!IsNotNullOrWhiteSpace(pilota.Nome))
                throw new ArgumentException("il nome non può essere vuoto", nameof(pilota.Nome));
            if (!IsNotNullOrWhiteSpace(pilota.Nazionalita))
                throw new ArgumentException("la nazionalita non puo essere null", nameof(pilota.Nazionalita));
            if (!IsPositiveInt(pilota.Eta))
                throw new ArgumentException("l'eta deve essere maggiore di 0", nameof(pilota.Eta));
            if (!IsPositiveInt(pilota.SquadraId))
                throw new ArgumentException("L'Id Squadra deve essere positivo", nameof(pilota.SquadraId));
        }

        public static void MotoValidation(Moto moto)
        {
            if (moto == null)
                throw new ArgumentNullException(nameof(moto), "la moto non può essere null");
            if (!IsNotNullOrWhiteSpace(moto.Marca))
                throw new ArgumentException("La marca della moto non può essere vuota", nameof(moto.Marca));
            if (!IsNotNullOrWhiteSpace(moto.Modello))
                throw new ArgumentException("il nome della moto non può essere vuota", nameof(moto.Modello));
            if (!IsPositiveInt(moto.SquadraId))
                throw new ArgumentException("L'Id Squadra deve essere positivo", nameof(moto.SquadraId));
        }
        public static void CircuitoValidation(Circuito circuito)
        {
            if (circuito == null)
                throw new ArgumentNullException(nameof(circuito), "il circuito non può essere null");
            if (!IsNotNullOrWhiteSpace(circuito.Nome))
                throw new ArgumentException("il nome non può essere vuoto", nameof(circuito.Nome));
            if (!IsNotNullOrWhiteSpace(circuito.Localita))
                throw new ArgumentException("La localita non può essere vuota", nameof(circuito.Localita));
            if (!IsPositiveInt(circuito.Lunghezza))
                throw new ArgumentException("la lunghezza deve essere maggiore di 0", nameof(circuito.Lunghezza));
        }
        /*
                public static void PurchaseValidation(Purchase purchase)
                {
                    if (purchase == null)
                        throw new ArgumentNullException(nameof(purchase), "il prodotto non può essere null");
                    if (!IsPositiveInt(purchase.Quantity))
                        throw new ArgumentException("quantità non valida");
                }

                public static bool IsValidDurata(string durata)
                {
                    if (!durata.Contains(":") && !IsNotNullOrWhiteSpace(durata))
                        return false;
                    durata = durata.Replace(":", "");
                    return int.TryParse(durata, out _);
                }
            }
        */
    }
}    