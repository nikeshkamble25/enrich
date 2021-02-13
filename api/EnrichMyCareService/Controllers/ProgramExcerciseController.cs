using EnrichMyCare.DataEntities;
using EnrichMyCare.EnrichMyCareService.Infrastructure;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnrichMyCareService.ViewModel;

namespace EnrichMyCare.EnrichMyCareService.Controllers
{
    /// <summary>
    /// Controller for adding Excercise and its related data
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProgramExcerciseController : ControllerBase
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramRepository _programRepository;
        private readonly IExcerciseRepository _excerciseRepository;
        private readonly IPatientProgramRepository _patientProgramRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Program Excercise Controller's Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="programRepository"></param>
        /// <param name="excerciseRepository"></param>
        /// <param name="patientProgramRepository"></param>
        public ProgramExcerciseController(IUnitOfWork unitOfWork, IProgramRepository programRepository,
           IExcerciseRepository excerciseRepository, IPatientProgramRepository patientProgramRepository)
        {
            _unitOfWork = unitOfWork;
            _programRepository = programRepository;
            _excerciseRepository = excerciseRepository;
            _patientProgramRepository = patientProgramRepository;
        }

        #endregion

        #region Public Action Methods

        /// <summary>
        /// Retrieves all program excercises
        /// </summary>
        [HttpGet(Name = "GetAllProgramExcercise")]
        public async Task<IActionResult> GetAllProgramExcercise()
        {
            IEnumerable<Program> response = await _programRepository.GetAllAsync();

            if (response == null)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        ///  Retrieves all program excercises by program Id
        /// </summary>
        /// <param name="programId"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetProgramById")]
        public async Task<IActionResult> GetProgramById(int? programId)
        {
            //Check whether Program Id is not null
            if (programId.HasValue)
            {
                return Ok(await _programRepository.GetByIdAsync(programId.Value));
            }
            else
                return BadRequest(EnrichMyCare_Messages.ProgramIdIsNull);
        }

        /// <summary>
        /// Get all the programs by Patient Id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetProgramByPatientId")]
        public async Task<IActionResult> GetProgramByPatientId(int? patientId)
        {
            //Check whether Program Id is not null
            if (patientId.HasValue)
            {
                return Ok(await _programRepository.GetProgramByPatientId(patientId.Value));
            }
            else
                return BadRequest(EnrichMyCare_Messages.PatientIdIsNull);
        }


        /// <summary>
        /// Method for adding Program and Excercise in the data store
        /// </summary>
        /// <param name="program"></param>
        /// <param name="excerciseId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddProgram([FromForm(Name = "program")] ProgramViewModel program)
        {

        }

        #endregion
    }
}
