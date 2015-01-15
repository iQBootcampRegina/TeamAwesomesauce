namespace ProductService
{
	public class Dimensions
	{
		public Dimensions(decimal height, decimal width, decimal length)
		{
			Height = height;
			Width = width;
			Length = length;
		}

		public decimal Height { get; set; }
		public decimal Width { get; set; }
		public decimal Length { get; set; }

	}
}