using EnrichMyCare.DataEntities;
using EnrichMyCare.EnrichMyCareService.Infrastructure;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EnrichMyCare.EnrichMyCareService.Controllers
{
    /// <summary>
    /// Controller for managing the Excercise, Initial Progression, Progression and ImageVideos
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExcerciseController : ControllerBase
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IExcerciseRepository _excerciseRepository;
        private readonly IInitialProgressionRepository _initialProgressionRepository;
        private readonly IProgressionRepository _progressionRepository;
        private readonly IImageVideoRepository _imageVideoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        /// <summary>
        /// Adding all the required dependencies
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="excerciseRepository"></param>
        /// <param name="initialProgressionRepository"></param>
        /// <param name="progressionRepository"></param>
        /// <param name="imageVideoRepository"></param>
        public ExcerciseController(IUnitOfWork unitOfWork, 
            IExcerciseRepository excerciseRepository,
            IInitialProgressionRepository initialProgressionRepository, 
            IProgressionRepository progressionRepository,
            IImageVideoRepository imageVideoRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _excerciseRepository = excerciseRepository;
            _initialProgressionRepository = initialProgressionRepository;
            _progressionRepository = progressionRepository;
            _imageVideoRepository = imageVideoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        /// <summary>
        /// Get all the excercise from the data store
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllExcercise")]
        public async Task<ActionResult<Excercise>> GetAllExcercise()
        {
            var response = await _excerciseRepository.GetAllAsync();

            if (response == null)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Get excercise by excersieId from the data store
        /// </summary>
        /// <param name="exerciseId"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetExerciseById")]
        public async Task<IActionResult> GetExerciseById(int? exerciseId)
        {
            if (exerciseId.HasValue == false)
                return BadRequest(EnrichMyCare_Messages.ExcerciseIdIsNull);

            var response = await _excerciseRepository.GetByIdAsync(exerciseId.Value);

            if (response == null)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Get exercise by program id from the data store
        /// </summary>
        /// <param name="programId"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetExerciseByProgramId")]
        public async Task<IActionResult> GetExerciseByProgramId(int? programId)
        {
            if (programId.HasValue)
            {
                //return Ok(await _excerciseRepository.GetExerciseByProgramId(programId.Value));
                var exerciseList = await _excerciseRepository.GetExerciseByProgramId(programId.Value);

                foreach(Excercise exer in exerciseList)
                {
                    foreach(ImageVideo img in exer.ImageVideo)
                    {
                        var filePath = img.ImageVideoPath;
                        var baseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                        var baseUrl = _httpContextAccessor.HttpContext.Request.IsHttps == true ? @"https://" : @"http://";
                        baseUrl += _httpContextAccessor.HttpContext.Request.Host.Value + @"/images";
                        img.ImageVideoPath = filePath.Replace(baseFilePath, baseUrl).Replace('\\', '/');
                    }
                }

                return Ok(exerciseList);
            }
            else
                return BadRequest(EnrichMyCare_Messages.ProgramIdIsNull);
        }
        
        /// <summary>
        /// Add excercise details in data store
        /// </summary>
        /// <param name="excercise"></param>
        /// <param name="imageVideoIds"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddExcercise")]
        public async Task<IActionResult> AddExcercise([FromBody] Excercise excercise, [FromQuery] List<int> imageVideoIds)
        {
            //Add Excercise details in database
            await _excerciseRepository.AddAsync(excercise);

            //Add Initial Progression details in database

            await _initialProgressionRepository.AddAsync(excercise.InitialProgression);

            //Add all progression details in database
            foreach (Progression prog in excercise.InitialProgression.Progression)
            {
                await _progressionRepository.AddAsync(prog);
            }

            await _unitOfWork.SaveChangesAsync();

            foreach (int imgId in imageVideoIds)
            {
                ImageVideo imgVid = new ImageVideo();
                imgVid.ImageVideoId = imgId;
                imgVid.ExcerciseId = excercise.ExcerciseId;
                await _imageVideoRepository.UpdateAsync(imgVid);
            }

            await _unitOfWork.SaveChangesAsync();

            return Ok(excercise.ExcerciseId);
        }
    }
}
