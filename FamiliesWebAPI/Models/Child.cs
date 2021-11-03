using System.Collections.Generic;

namespace FamiliesWebAPI.Models {
public class Child : Person {
    
    public IList<Interest> Interests { get; set; }
    public IList<Pet> Pets { get; set; }
}
}