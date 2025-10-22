using System.Collections.Generic;

namespace DontMissVulcan.Models.Recruitment.Data.Dto
{
	internal class OperatorDto
	{
		public string? Name { get; set; }
		public int Rarity { get; set; }
		public string? Class { get; set; }
		public string? Position { get; set; }
		public List<string>? Specializations { get; set; }
	}
}
