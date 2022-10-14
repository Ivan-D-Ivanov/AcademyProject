namespace AcademyProjectCaches.CacheInMemoryCollection
{
    public class GenericCollection<TValaue>
    {
        private static List<TValaue> _list = new List<TValaue>();

        public async Task<IEnumerable<TValaue>> GetAllItems()
        {
            return _list;
        }

        public TValaue Add(TValaue value)
        {
            _list.Add(value);
            return value;
        }
    }
}
