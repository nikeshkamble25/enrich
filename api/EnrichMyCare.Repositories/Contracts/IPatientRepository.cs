using EnrichMyCare.DataEntities;

namespace EnrichMyCare.Repositories.Contracts
{
    /// <summary>
    /// Interface for CRUD logic for patient Data Entity
    /// </summary>
    public interface IPatientRepository : IAsyncRepository<Patient>
    {
        //Task<IEnumerable<Program>> GetProgramByPatientId(int patientId);
    }
}
