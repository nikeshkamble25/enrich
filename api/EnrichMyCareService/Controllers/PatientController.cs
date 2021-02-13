using EnrichMyCare.DataEntities;
using EnrichMyCare.EnrichMyCareService.Infrastructure;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrichMyCare.EnrichMyCareService.Controllers
{
    /// <summary>
    /// Controller for managing patient details
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PatientController : ControllerBase
    {
        #region Fields

        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Patient Controller Constructor
        /// </summary>
        /// <param name="patientRepository"></param>
        public PatientController(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Action Methods

        /// <summary>
        /// Get All Patients List
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            IEnumerable<Patient> response = null;
            response = await _patientRepository.GetAllAsync();

            if (response == null)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Get patient details by patient Id from the data store
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetPatientsById")]
        public async Task<IActionResult> GetPatientsById(int? patientId)
        {
            Patient response = null;
            if (patientId.HasValue)
            {
                response = await _patientRepository.GetByIdAsync(patientId.Value);

                if (response == null)
                    return BadRequest(response);

                return Ok(response);
            }
            else
                return BadRequest(EnrichMyCare_Messages.PatientIdIsNull);
        }

        /// <summary>
        /// Add patient in data store
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddPatient")]
        [Produces("application/json")]
        public async Task AddPatient([FromBody] Patient patient)
        {
            await _patientRepository.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Update patient in data store
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPut(Name = "UpdatePatient")]
        [Produces("application/json")]
        public async Task UpdatePatient([FromBody] Patient patient)
        {
            await _patientRepository.UpdateAsync(patient);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Remove patient from data store
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpDelete]
        [Produces("application/json")]
        public async Task DeletePatient([FromBody] Patient patient)
        {
            await _patientRepository.RemoveAsync(patient);
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}
