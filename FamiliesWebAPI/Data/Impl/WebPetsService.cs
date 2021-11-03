﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;

namespace FamiliesWebAPI.Data.Impl
{
    public class WebPetsService : IPetsService
    {
        public IList<Pet> Pets { get; private set; }
        private readonly IFamiliesService familiesService;

        public WebPetsService(IFamiliesService familiesService)
        {
            this.familiesService = familiesService;
            GetPetsAsync();
        }
        
        public async Task<IList<Pet>> GetPetsAsync()
        {
            Pets = new List<Pet>();
            foreach (var family in await familiesService.GetFamiliesAsync())
            {
                foreach (var pet in family.Pets)
                {
                    Pets.Add(pet);
                }
            }
            return Pets;
        }

        public async Task<Pet> AddPetAsync(int familyId, Pet pet)
        {
            Pets.Add(pet);
            Family family = familiesService.GetFamilyById(familyId);
            family.Pets.Add(pet);
            await familiesService.UpdateFamilyAsync(family);
            return pet;
        }

        public async Task RemovePetAsync(int familyId, int id)
        {
            Pet toRemove = Pets.First(p => p.Id == id);
            Pets.Remove(toRemove);
            Family family = familiesService.GetFamilyById(familyId);
            family.Pets.Remove(toRemove);
            await familiesService.UpdateFamilyAsync(family);
        }

        public async Task<Pet> UpdatePetAsync(int familyId, Pet pet)
        {
            Pet toUpdate = Pets.First(p => p.Id == pet.Id);
            toUpdate.Species = pet.Species;
            toUpdate.Name = pet.Name;
            toUpdate.Age = pet.Age;
            await familiesService.UpdateFamilyAsync(familiesService.GetFamilyById(familyId));
            return toUpdate;
        }

        public IList<Pet> getFamilyPets(int? familyId)
        {
            IList<Pet> pets = new List<Pet>();
            foreach (var p in familiesService.GetFamilyById(familyId).Pets)
            {
                pets.Add(p);
            }
            return pets;
        }
    }
}