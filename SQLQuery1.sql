Select Pet.Name, PetType.PetTypeName From Pet Inner Join PetType On Pet.TypeId = PetType.Id Where PetType.Id = 2
Inner Join PetType
On Pet.TypeId = PetType.Id
Where PetType.Id = 2