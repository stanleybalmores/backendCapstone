namespace CapstoneDb.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }        

        internal byte[] PhotoData;

    }

    public class PhotoDTO     
    {
            public string Title { get; set; }
            public IFormFile PhotoFile { get; set; }
    }
}
