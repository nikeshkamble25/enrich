using EnrichMyCare.DataEntities;
using EnrichMyCare.EnrichMyCareService.Infrastructure;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EnrichMyCare.EnrichMyCareService.Controllers
{
    /// <summary>
    /// Image/Video Upload Controller class
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageVideoUploadController : ControllerBase
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageVideoRepository _imageVideoRepository;
        private string _fileName;
        private string _path;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        /// <summary>
        /// Image Video Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="imageVideoRepository"></param>
        /// <param name="httpContextAccessor"></param>
        public ImageVideoUploadController(IUnitOfWork unitOfWork,
            IImageVideoRepository imageVideoRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _imageVideoRepository = imageVideoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Public Service Methods

        /// <summary>
        /// Image and Video Upload service method
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost(Name = "UploadImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage([FromForm(Name = "File")] IFormFile file)
        {
            ImageVideo imgVid = new ImageVideo();

            if (CheckIfExcelFile(file))
            {
                await WriteFile(file);

                imgVid.ImageVideoPath = _path;
                imgVid.IsActive = true;
                await _imageVideoRepository.AddAsync(imgVid);
                await _unitOfWork.SaveChangesAsync();
            }
            else
                return BadRequest(new { message = EnrichMyCare_Messages.InvalidExtension });

            return Ok(imgVid.ImageVideoId);
        }

        /// <summary>
        /// Returns the image/video file from data store
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetImageVideoByImageId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetImageVideoByImageId(int? imageId)
        {
            if (imageId.HasValue == false)
                return BadRequest(EnrichMyCare_Messages.ImageVideoIdIsNull);

            var img = await _imageVideoRepository.GetByIdAsync(imageId.Value);
            var filePath = img.ImageVideoPath;
            var baseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

            var baseUrl = _httpContextAccessor.HttpContext.Request.IsHttps == true ? @"https://" : @"http://";
            baseUrl += _httpContextAccessor.HttpContext.Request.Host.Value + @"/images";
            filePath = filePath.Replace(baseFilePath, baseUrl).Replace('\\', '/');

            return Ok(new List<String>() { filePath });
        }

        /// <summary>
        /// Get all the images by Excercise Id from the data store
        /// </summary>
        /// <param name="excerciseId"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetImageVideoByExcerciseId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetImageVideoByExcerciseId(int? excerciseId)
        {
            List<string> imageUrlList = new List<string>();

            if (excerciseId.HasValue == false)
                return BadRequest(EnrichMyCare_Messages.ExcerciseIdIsNull);

            var imgList = await _imageVideoRepository.GetWhereAsync(img => img.ExcerciseId == excerciseId.Value);

            if (imgList != null)
            {
                foreach (ImageVideo img in imgList)
                {
                    var filePath = img.ImageVideoPath;
                    var baseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                    var baseUrl = _httpContextAccessor.HttpContext.Request.IsHttps == true ? @"https://" : @"http://";
                    baseUrl += _httpContextAccessor.HttpContext.Request.Host.Value + @"/images";
                    filePath = filePath.Replace(baseFilePath, baseUrl).Replace('\\', '/');
                    imageUrlList.Add(filePath);
                }
            }
            else
                BadRequest(EnrichMyCare_Messages.ImageVideoNotFoundByExcerciseId);

            return Ok(imageUrlList);
        }

        /// <summary>
        /// Remove images from the data store by ImageVideo Id
        /// </summary>
        /// <param name="imageVideoId"></param>
        /// <returns></returns>
        [HttpDelete("RemoveByImageVideoId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveByImageVideoId(int? imageVideoId)
        {
            if (imageVideoId.HasValue)
            {
                var imgVideo = await _imageVideoRepository.GetByIdAsync(imageVideoId.Value);
                var filePath = imgVideo.ImageVideoPath;
                await _imageVideoRepository.RemoveAsync(imgVideo);

                //Check whether the path exists?
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                return Ok(EnrichMyCare_Messages.ImageVideoDeletedById);
            }
            else
                return BadRequest(EnrichMyCare_Messages.ImageVideoNotFoundById);
        }

        /// <summary>
        /// Remove images from the data store by Exercise Id
        /// </summary>
        /// <param name="exerciseId"></param>
        /// <returns></returns>
        [HttpDelete("RemoveByExerciseId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveByExerciseId(int? exerciseId)
        {
            //Check whether Exercise Id is not null
            if (exerciseId.HasValue)
            {
                var imgVideoList = await _imageVideoRepository.GetWhereAsync(img => img.ExcerciseId == exerciseId.Value);

                //Check whether the result is not null & ImageVideoList has data
                if (imgVideoList != null && imgVideoList?.Count() > 0)
                {
                    foreach (ImageVideo img in imgVideoList)
                    {
                        var filePath = img.ImageVideoPath;
                        await _imageVideoRepository.RemoveAsync(img);

                        if ((System.IO.File.Exists(filePath)))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }

                return Ok(EnrichMyCare_Messages.ImageVideoDeletedById);
            }
            else
                return BadRequest(EnrichMyCare_Messages.ExcerciseIdIsNull);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Check the extension of uploaded files
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            return extension == ".jpg" || extension == ".mpeg" || extension == ".gif" || extension == ".mp4"
                || extension == ".avi" || extension == ".mov"; // Change the extension based on your need
        }

        /// <summary>
        /// Method for writing file to a file system
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<bool> WriteFile(IFormFile file)
        {
            bool isSaveSuccess = false;

            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            _fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }

            _path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
               _fileName);

            using (var stream = new FileStream(_path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            isSaveSuccess = true;

            return isSaveSuccess;
        }

        #endregion
    }
}
