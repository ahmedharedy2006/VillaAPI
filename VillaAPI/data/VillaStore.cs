using System.Xml.Linq;
using VillaAPI.Models.DTO;

namespace VillaAPI.data
{
    public class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
            {
                new VillaDTO() { Id = 1 , Name = "Pool View"},
                new VillaDTO() { Id = 2 , Name = "Beach View"}
    };
}
}
