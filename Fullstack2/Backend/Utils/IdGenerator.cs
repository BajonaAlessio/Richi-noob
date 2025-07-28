namespace Backend.Utils
{
    public static class IdGenerator
    {
        //sfrutto l'ereditarietà per evitare di ripetere il codice in ogni servizio
        public static int GenNextId<T>(List<T> items) where T : IIdentifiable
        {
            if (items.Count == 0)
                return 1;

            int maxId = 0;
            foreach (var item in items)
            {
                if (item.Id > maxId)
                    maxId = item.Id;
            }
            return maxId + 1;
        }
    }

    public interface IIdentifiable
    {
        int Id { get; set; }
    }
}