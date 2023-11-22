namespace PSCS.OrderingSystem.Models
{
    public class ProductAddEditViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SupplierId { get; set; }
        public IFormFile? Image { get; set; }
        public byte[]? ImageBytes { get; set; }

        public string ConvertByteArrayToBase64()
        {
            if (ImageBytes != null)
            {
                return "data:image/png;base64," + Convert.ToBase64String(ImageBytes);
            }
            return string.Empty;
        }
    }
}
