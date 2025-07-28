using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class PurchaseService
    {
        private readonly List<Purchase> _purchases = new();

        public string path;

        public PurchaseService()
        {
            ReadConfig();
            _purchases = JsonFileHelper.LoadList<Purchase>(path);
        }

        public void Save(Purchase purchase)
        {
            JsonFileHelper.SaveList(path, purchase);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Config.txt");
            //riga 1 di config: path salvataggio albums
            path = lines[2].Replace("path_Purchase:", "");
        }

        public List<Purchase> GetAll()
        {
            List<Purchase> result = new List<Purchase>();
            foreach (var purchase in _purchases)
            {
                result.Add(purchase);
            }
            return result;
        }

        public Purchase? GetById(int id)
        {
            foreach (var purchase in _purchases)
            {
                if (purchase.Id == id)
                {
                    return purchase;
                }
            }
            return null;
        }

        public Purchase Add(Purchase newPurchase)
        {
            //ValidationHelper.PurchaseValidation(newPurchase);
            newPurchase.Id = IdGenerator.GenNextId<Purchase>(_purchases);
            _purchases.Add(newPurchase);
            LoggerHelper.Log($"Aggiunto purchase: {newPurchase.Id}");
            Save(newPurchase);
            return newPurchase;
        }

        public bool Update(int id, Purchase updatedPurchase)
        {
            //ValidationHelper.PurchaseValidation(updatedPurchase);
            Purchase? existing = null;
            foreach (var a in _purchases) //sostituire con getbyid?
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscito l'aggiornamento di purchase: {updatedPurchase.Id}");  
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Purchase>(path, existing);

            existing.UserId = updatedPurchase.UserId;
            existing.AlbumId = updatedPurchase.AlbumId;
            existing.Quantity = updatedPurchase.Quantity;
            
            File.Delete(correctPath);
            LoggerHelper.Log($"Riuscito l'aggiornamento di purchase: {existing.Id}");  
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Purchase purchase = GetById(id);
            if (purchase is null)
            {
                LoggerHelper.Log($"Non riuscita la rimozione di purchase: {purchase.Id}");
                return false;
            }
                

            string correctPath = JsonFileHelper.WritePath<Purchase>(path, purchase);
            File.Delete(correctPath);
            LoggerHelper.Log($"Riuscita rimozione di purchase: {purchase.Id}");
            _purchases.Remove(purchase);
            return true;
        }
    }
}