using Backend.Models;

namespace Backend.Utils
{
    public class ValidationHelper
    {
        public static bool IsNotNullOrWhiteSpace(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }

        public static bool IsPositiveDecimal(decimal value)
        {
            return value > 0;
        }

        public static bool IsPositiveInt(int value)
        {
            return value > 0;
        }

        public static bool IsValidCAP(string cap)
        {
            return !string.IsNullOrWhiteSpace(cap) && cap.Length == 5 && int.TryParse(cap, out _);
        }

        public static void CanzoneValidation(Canzone canzone)
        {
            if (canzone == null)
                throw new ArgumentNullException(nameof(canzone), "la canzone non può essere null");
            if (!IsNotNullOrWhiteSpace(canzone.Nome))
                throw new ArgumentNullException(nameof(canzone.Nome), "il nome della canzone non può essere null");
            if (IsValidDurata(canzone.Durata))
                throw new ArgumentException("durata non valida");
        }

        public static void AlbumValidation(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album), "il prodotto non può essere null");
            if (!IsNotNullOrWhiteSpace(album.Titolo))
                throw new ArgumentNullException(nameof(album.Titolo), "il titolo non può essere null");
            if (!IsPositiveInt(album.Anno))
                throw new ArgumentException("Anno troppo antico");
            if (!IsNotNullOrWhiteSpace(album.Autore))
                throw new ArgumentNullException(nameof(album.Titolo), "l'autore non può essere null");
            if (!album.CanzoniId.Any())
                throw new ArgumentException("Lista di canzoni vuota");
            if (!IsNotNullOrWhiteSpace(album.Genere))
                throw new ArgumentNullException(nameof(album.Genere), "il genere non può essere null");
        }

        public static void PurchaseValidation(Purchase purchase)
        {
            if (purchase == null)
                throw new ArgumentNullException(nameof(purchase), "il prodotto non può essere null");
            if (!IsPositiveInt(purchase.Quantity))
                throw new ArgumentException("quantità non valida");
        }

        public static void UserValidation(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "il prodotto non può essere null");
            if (!IsNotNullOrWhiteSpace(user.NomeUtente))
                throw new ArgumentNullException(nameof(user.NomeUtente), "il nome utente non può essere null");
            if (!IsValidAddress(user.Indirizzo))
                throw new ArgumentNullException(nameof(user.Indirizzo), "indirizzo non valido");
        }

        public static bool IsValidAddress(Models.Indirizzo address)
        {
            return address != null &&
            IsNotNullOrWhiteSpace(address.Citta) &&
            IsNotNullOrWhiteSpace(address.Via) &&
            IsValidCAP(address.Cap);
        }

        public static bool IsValidDurata(string durata)
        {
            if (!durata.Contains(":") && !IsNotNullOrWhiteSpace(durata))
                return false;
            durata = durata.Replace(":", "");
            return int.TryParse(durata, out _);
        }
    }
}